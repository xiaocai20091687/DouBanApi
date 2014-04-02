using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using LuoWangDemo.DataProvider;
using LuoWangDemo.Helper;
using LuoWangDemo.Resources;
using LuoWangDemo.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LuoWangDemo.View
{
    public partial class DouBanMidPage : PhoneApplicationPage
    {
        #region

        private UserLoginDataProvider _userLoginDataProvider;
        #endregion
        public DouBanMidPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userLoginDataProvider = new UserLoginDataProvider();
            string requestUrl = _userLoginDataProvider.GetAuthorizationCodeRequestUrl();

            this.Authorize_WB.Navigate(new Uri(requestUrl, UriKind.RelativeOrAbsolute));
        }

        private void Authorize_WB_OnNavigating(object sender, NavigatingEventArgs e)
        {
            string url = e.Uri.ToString();
            string code = string.Empty;

            #region Ckech Network

            if (!CommonTool.CheckNetworkStatus())
            {
                CommonTool.ShowCoding4FunToastNotify(AppResources.NetworkWrong,AppResources.Waring);
                return;
            }

            try
            {
                if (url.Contains("code"))
                {
                    string[] aplitUrlArrey = url.Split(new char[] {'?', '='});
                    if(aplitUrlArrey.Length==0)
                        return;
                    for (int i = 0; i < aplitUrlArrey.Length; i++)
                    {
                        if (aplitUrlArrey[i].Trim().ToLower() == "code")
                        {
                            code = aplitUrlArrey[i];
                            break;
                        }
                    }

                    if(string.IsNullOrEmpty(code))
                        return;

                    //Get Access Token
                    _userLoginDataProvider.GetAccessTokenAction("authorization_code",code);
                    _userLoginDataProvider.GetAccessTokenCompleted += (responseData,ex) =>
                    {
                        AccessTokenContent accessTokenContent= responseData as AccessTokenContent;
                        if (ex==null&&accessTokenContent!=null)
                        {
                            if (accessTokenContent.StatusCode == ResponseStatusCode.Success && !string.IsNullOrEmpty(accessTokenContent.AccessTokenInfo.AccessToken))
                            {
                                _userLoginDataProvider.GetUserInformationAction(accessTokenContent.AccessTokenInfo.AccessToken);
                                _userLoginDataProvider.GetUserInformationCompleted += (userInforResponseData,arg) =>
                                {
                                    UserInformationContent userInformationContent= userInforResponseData as UserInformationContent;
                                    if (arg == null && userInformationContent!=null)
                                    {
                                        if (userInformationContent.StatusCode==ResponseStatusCode.Success)
                                        {
                                            IsoSettingHelper.IsolatedStorageSaveObject("UserInfo",userInformationContent.UserInformationInfo);
                                            //NavigationService.Navigate(new Uri("/View/UserProfilePage.xaml",
                                                //UriKind.RelativeOrAbsolute));
                                        }
                                        else if(userInformationContent.StatusCode==ResponseStatusCode.Fail)
                                        {
                                            //NavigationService.Navigate(new Uri("/View/LoginPage.xaml", UriKind.RelativeOrAbsolute));
                                            CommonTool.ShowCoding4FunToastNotify(AppResources.LoginFailed, "");
                                        }
                                    }
                                };
                            }
                            else if (accessTokenContent.StatusCode==ResponseStatusCode.Fail)
                            {
                                //NavigationService.Navigate(new Uri("/View/LoginPage.xaml", UriKind.RelativeOrAbsolute));
                                CommonTool.ShowCoding4FunToastNotify(AppResources.LoginFailed,"");
                            }
                        }
                    };
                }
            }
            catch (Exception)
            {

            }
            #endregion
        }

        private void Authorize_WB_OnNavigated(object sender, NavigationEventArgs e)
        {
            

        }
    }
}
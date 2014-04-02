using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuoWangDemo.Helper;
using LuoWangDemo.Model;
using LuoWangDemo.ViewModel;
using Newtonsoft.Json;
using RestSharp;

namespace LuoWangDemo.DataProvider
{
    public class UserLoginDataProvider : BaseDataProvider
    {
        #region Property

        private readonly string ClientId = "02301995a64dccef06904cc0f2e966cd";
        private readonly string RedirectUri = "http://www.moweather.com";
        private readonly string Secret = "bbd0e9469b9397b3";

        public event EventHandler GetAccessTokenCompleted;
        public event EventHandler GetUserInformationCompleted;
        #endregion

        #region Get Authorization Code

        public string GetAuthorizationCodeRequestUrl()
        {
            string authorizationCodeRequestUrl = base.GetRequestUrlFromConfig(CommentType.AuthorizationCode);
            authorizationCodeRequestUrl += "client_id=" + ClientId;
            authorizationCodeRequestUrl += "&redirect_uri=" + RedirectUri;
            authorizationCodeRequestUrl += "&response_type=code";
            authorizationCodeRequestUrl += "&scope=shuo_basic_r,shuo_basic_w,douban_basic_common";
            return authorizationCodeRequestUrl;
        }
        #endregion

        #region Get Access Token

        private string GetAccessTokenRequestUrl(string grantType, string authorizationCode)
        {
            string accessTokenRequestUrl = base.GetRequestUrlFromConfig(CommentType.AccessToken);
            accessTokenRequestUrl += "client_id=" + ClientId;
            accessTokenRequestUrl += "&client_secret=" + Secret;
            accessTokenRequestUrl += "&redirect_uri=" + RedirectUri;
            accessTokenRequestUrl += "&grant_type=" + grantType;
            accessTokenRequestUrl += "&code=" + authorizationCode;
            return accessTokenRequestUrl;
        }

        public void GetAccessTokenAction(string grantType, string authorizationCode)
        {
            string accessTokenRequestUrl = GetAccessTokenRequestUrl(grantType, authorizationCode);
            HttpDataRequestHelper accessTokenDataRequestHelper = new HttpDataRequestHelper();
            accessTokenDataRequestHelper.ExcuteAsyncRequest(accessTokenRequestUrl, Method.POST);
            accessTokenDataRequestHelper.AsyncRequestCompleted += (responseContent, ex) =>
            {
                #region Handle Access Token Response
                if (ex == null)
                {
                    AccessTokenContent accessTokenContent = new AccessTokenContent();
                    if (accessTokenDataRequestHelper.ResponseContent.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        #region Request Success

                        try
                        {
                            #region Parse Success
                            accessTokenContent.AccessTokenInfo = JsonConvert.DeserializeObject<AccessTokenModel>(responseContent.ToString());
                            accessTokenContent.StatusCode = ResponseStatusCode.Success;
                            #endregion
                        }
                        catch (Exception)
                        {
                            #region Parse Failed

                            accessTokenContent.StatusCode = ResponseStatusCode.Fail;
                            accessTokenContent.ErrorCode = ResponseErrorCode.ParseFailed;

                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region Request Failed

                        accessTokenContent.StatusCode = ResponseStatusCode.Fail;
                        accessTokenContent.ErrorCode = ResponseErrorCode.DataRequestFailed;

                        #endregion
                    }

                    if (GetAccessTokenCompleted != null)
                        GetAccessTokenCompleted(accessTokenContent, null);
                }
                #endregion
            };
        }

        #endregion

        #region Get User Information

        private string GetUserInformationRequestUrl(string accessToken)
        {
            string userInformationRequestUrl = base.GetRequestUrlFromConfig(CommentType.UserLogin);
            userInformationRequestUrl += "Authorization: Bearer =" + accessToken;
            return userInformationRequestUrl;
        }

        public void GetUserInformationAction(string accessToken)
        {
            string requestUrl = GetUserInformationRequestUrl(accessToken);
            HttpDataRequestHelper userInformationDataRequestHelper = new HttpDataRequestHelper();
            userInformationDataRequestHelper.ExcuteAsyncRequest(requestUrl, Method.GET);
            userInformationDataRequestHelper.AsyncRequestCompleted += (responseContent, ex) =>
            {
                #region Handle User Information Response
                if (ex == null)
                {
                    UserInformationContent userInformationContent = new UserInformationContent();
                    if (userInformationDataRequestHelper.ResponseContent.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        #region Data Request Success

                        try
                        {
                            userInformationContent.UserInformationInfo = JsonConvert.DeserializeObject<UserInformationModel>(responseContent.ToString());
                            userInformationContent.StatusCode = ResponseStatusCode.Success;
                        }
                        catch (Exception)
                        {
                            userInformationContent.StatusCode = ResponseStatusCode.Fail;
                            userInformationContent.ErrorCode = ResponseErrorCode.ParseFailed;
                        }

                        #endregion
                    }
                    else
                    {
                        #region  Data Request Failed

                        userInformationContent.StatusCode = ResponseStatusCode.Fail;
                        userInformationContent.ErrorCode = ResponseErrorCode.DataRequestFailed;

                        #endregion
                    }

                    if (GetUserInformationCompleted != null)
                        GetUserInformationCompleted(userInformationContent,null);
                }
                #endregion
            };
        }

        #endregion
    }
}

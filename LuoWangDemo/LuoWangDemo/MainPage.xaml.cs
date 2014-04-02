using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using LuoWangDemo.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LuoWangDemo.Resources;

namespace LuoWangDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MainViewModel _mainViewModel;
        private ApplicationBarIconButton _loginApplicationBarIconButton;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if(_mainViewModel==null)
                _mainViewModel=new MainViewModel();
            _mainViewModel.SetMyProfileSource();
        }

        #region Action

        private void GetData()
        {
            if (_mainViewModel == null)
                _mainViewModel = new MainViewModel();
            _mainViewModel.GetImageSourceByIndex(this.LuoWang_Pivot.SelectedIndex);
            this.DataContext = _mainViewModel;
        }

        private void LuoWang_Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetData();
        }

        private void Music_LB_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox selectedListBox= sender as ListBox;
            if (selectedListBox!=null)
            {
                ComentModel commentModel= selectedListBox.SelectedItem as ComentModel;
                
                if(commentModel==null)
                    return;
                NavigationService.Navigate(new Uri("/View/DetailPage.xaml?TargetUrl="+commentModel.CommentLinkUrl, UriKind.RelativeOrAbsolute));
            }
        }
        #endregion

        private void Login_OnTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LuoWangDemo.View
{
    public partial class DetailPage : PhoneApplicationPage
    {
        private string _targetUrl = string.Empty;

        public DetailPage()
        {
            InitializeComponent();
            this.Loaded += DetailPage_Loaded;
        }

        void DetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            Detail_WB.Navigate(new Uri(_targetUrl,UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            IDictionary<string,string> queryArguments = this.NavigationContext.QueryString;

            if (queryArguments!=null)
            {
                foreach (KeyValuePair<string,string> item in queryArguments)
                {
                    if (item.Key.Equals("TargetUrl"))
                    {
                        _targetUrl = item.Value;
                    }
                }
            }
        }
    }
}
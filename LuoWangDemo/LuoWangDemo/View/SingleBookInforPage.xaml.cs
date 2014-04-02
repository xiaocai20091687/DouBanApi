using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using LuoWangDemo.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LuoWangDemo.View
{
    public partial class SingleBookInforPage : PhoneApplicationPage
    {
        private SingleBookInforViewModel _singleBookInforViewModel;

        public SingleBookInforPage()
        {
            InitializeComponent();
            this.Loaded += SingleBookInforPage_Loaded;
        }

        void SingleBookInforPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (_singleBookInforViewModel==null)
              _singleBookInforViewModel=new SingleBookInforViewModel();

            _singleBookInforViewModel.GetSingleBookInforData();
        }
    }
}
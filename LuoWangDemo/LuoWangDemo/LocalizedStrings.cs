using System.ComponentModel;
using LuoWangDemo.Resources;

namespace LuoWangDemo
{
    /// <summary>
    /// 提供对字符串资源的访问权。
    /// </summary>
    public class LocalizedStrings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources
        {
            get
            {
                return _localizedResources;
            }
            set
            {
                _localizedResources = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LocalizedResources"));
            }
        }
    }
}
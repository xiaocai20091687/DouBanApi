using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Linq;

namespace LuoWangDemo.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void BindPropertyChangedEventHandler(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string GetImagePathByType(ImageType imageType)
        {
            string path = string.Empty;
            StreamResourceInfo imageConfig =
                Application.GetResourceStream(new Uri("/LuoWangDemo;component/ViewModel/ImagePathConfig.xml",
                    UriKind.RelativeOrAbsolute));

            XDocument imageXDocument = XDocument.Load(imageConfig.Stream);
            XElement queryXElement = null;

            if (imageXDocument != null)
            {
                switch (imageType)
                {
                    case ImageType.MusicImage:
                        queryXElement =
                            imageXDocument.Elements("ImagePath").First().Elements("MusicCommentBackground").First();
                        break;
                    case ImageType.MovieImage:
                        queryXElement =
                            imageXDocument.Elements("ImagePath").First().Elements("MovieCommentBackground").First();
                        break;
                    case ImageType.BookImage:
                        queryXElement =
                            imageXDocument.Elements("ImagePath").First().Elements("BookCommentBackground").First();
                        break;
                    case ImageType.NewCommentImage:
                        queryXElement =
                            imageXDocument.Elements("ImagePath").First().Elements("NewCommentBackground").First();
                        break;
                }
            }

            if (queryXElement != null)
                path = queryXElement.Attributes().Single(x => x.Name == "Path").Value;

            return path;
        }
    }

    public enum ImageType
    {
        MusicImage,
        MovieImage,
        BookImage,
        NewCommentImage
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Linq;

namespace LuoWangDemo.Helper
{
    public class BaseDataProvider
    {
        public string GetRequestUrlFromConfig(CommentType commentType)
        {
            string requestUrl = string.Empty;

            StreamResourceInfo configFile =
                Application.GetResourceStream(new Uri("/LuoWangDemo;component/DataProvider/UrlConfig.xml",
                    UriKind.RelativeOrAbsolute));

            XDocument configDoc = XDocument.Load(configFile.Stream);
            XElement queryElement = null;

            switch (commentType)
            {
                case CommentType.MusicComment:
                    queryElement = configDoc.Elements("RequestUrl").First().Elements("MusicCommentUrl").First();
                    break;
                case CommentType.MovieComment:
                    queryElement = configDoc.Elements("RequestUrl").First().Elements("MovieCommentUrl").First();
                    break;
                case CommentType.BookComment:
                    queryElement = configDoc.Elements("RequestUrl").First().Elements("BookCommentUrl").First();
                    break;
                case CommentType.NewComment:
                    queryElement = configDoc.Elements("RequestUrl").First().Elements("NewCommentUrl").First();
                    break;
                case CommentType.SingleBookInfor:
                    queryElement = configDoc.Elements("RequestUrl").First().Elements("SingleBookInfor").First();
                    break;
                case CommentType.AuthorizationCode:
                    queryElement = configDoc.Elements("RequestUrl").First().Elements("AutorizationCode").First();
                    break;
                case CommentType.AccessToken:
                    queryElement = configDoc.Elements("RequestUrl").First().Elements("AccessToken").First();
                    break;
                case CommentType.UserLogin:
                    queryElement = configDoc.Elements("RequestUrl").First().Elements("UserLogin").First();
                    break;
            }

            if (queryElement != null)
                requestUrl= queryElement.Attributes().Single(x => x.Name == "Url").Value;

            return requestUrl;
        }
    }

    public enum CommentType
    {
        MusicComment=0,
        MovieComment=1,
        BookComment=2,
        NewComment=3,
        SingleBookInfor,
        AuthorizationCode,
        AccessToken,
        UserLogin
    }

    public enum ResponseStatusCode
    {
        Success,
        Fail
    }

    public enum ResponseErrorCode
    {
        DataRequestFailed,
        ParseFailed
    }

    public enum ContentType
    {
        Title,
        Comment
    }
}

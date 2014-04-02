using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LuoWangDemo.Helper;
using LuoWangDemo.Model;
using LuoWangDemo.Resources;
using Microsoft.Phone.Shell;

namespace LuoWangDemo.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Property
        #region Source
        private List<ComentModel> _musicCommentSource;
        public List<ComentModel> MusicCommentSource
        {
            get { return this._musicCommentSource; }
            set { this._musicCommentSource = value; BindPropertyChangedEventHandler("MusicCommentSource"); }
        }


        private List<ComentModel> _movieCommentSource;
        public List<ComentModel> MovieCommentSource
        {
            get { return this._movieCommentSource; }
            set { this._movieCommentSource = value; BindPropertyChangedEventHandler("MovieCommentSource"); }
        }


        private List<ComentModel> _bookCommentSource;
        public List<ComentModel> BookCommentSource
        {
            get { return this._bookCommentSource; }
            set { this._bookCommentSource = value; BindPropertyChangedEventHandler("BookCommentSource"); }
        }


        private List<ComentModel> _newCommentSource;
        public List<ComentModel> NewCommentSource
        {
            get { return this._newCommentSource; }
            set { this._newCommentSource = value; BindPropertyChangedEventHandler("NewCommentSource"); }
        }

        private List<MyProfileModel> _myProfileSource;

        public List<MyProfileModel> MyProfileSource
        {
            get { return this._myProfileSource; }
            set { this._myProfileSource = value; BindPropertyChangedEventHandler("MyProfileSource"); }
        }

        private string _backgroundImage;
        public string BackgroundImage
        {
            get { return this._backgroundImage; }
            set { this._backgroundImage = value; BindPropertyChangedEventHandler("BackgroundImage"); }
        }

        #endregion
        #endregion

        public void GetImageSourceByIndex(int index)
        {
            switch (index)
            {
                case (int)CommentType.MusicComment:
                    BackgroundImage = base.GetImagePathByType(ImageType.MusicImage);
                    GetCommentData(CommentType.MusicComment);
                    break;
                case (int)CommentType.MovieComment:
                    BackgroundImage = base.GetImagePathByType(ImageType.MovieImage);
                    GetCommentData(CommentType.MovieComment);
                    break;
                case (int)CommentType.BookComment:
                    BackgroundImage = base.GetImagePathByType(ImageType.BookImage);
                    GetCommentData(CommentType.BookComment);
                    break;
                case (int)CommentType.NewComment:
                    BackgroundImage = base.GetImagePathByType(ImageType.NewCommentImage);
                    GetCommentData(CommentType.NewComment);
                    break;
            }
        }

        private void SetDataSource(List<ComentModel> source, CommentType commentType)
        {
            switch (commentType)
            {
                case CommentType.MusicComment:
                    if (MusicCommentSource == null)
                        MusicCommentSource = new List<ComentModel>();
                    MusicCommentSource = source;
                    break;
                case CommentType.MovieComment:
                    if (MovieCommentSource == null)
                        MovieCommentSource = new List<ComentModel>();
                    MovieCommentSource = source;
                    break;
                case CommentType.BookComment:
                    if (BookCommentSource == null)
                        BookCommentSource = new List<ComentModel>();
                    BookCommentSource = source;
                    break;
                case CommentType.NewComment:
                    if (NewCommentSource == null)
                        NewCommentSource = new List<ComentModel>();
                    NewCommentSource = source;
                    break;
            }
        }

        public void SetMyProfileSource()
        {
            if (MyProfileSource == null)
                MyProfileSource = new List<MyProfileModel>();
            MyProfileSource.Add(new MyProfileModel() { Title = "图书评论", Count = "24" });
            MyProfileSource.Add(new MyProfileModel() { Title = "电影评论", Count = "24" });
            MyProfileSource.Add(new MyProfileModel() { Title = "关注电台", Count = "24" });
            MyProfileSource.Add(new MyProfileModel() { Title = "好友评论", Count = "24" });
            MyProfileSource.Add(new MyProfileModel() { Title = "电影评论", Count = "24" });
            MyProfileSource.Add(new MyProfileModel() { Title = "关注电台", Count = "24" });
        }

        #region Get Source
        private void GetCommentData(CommentType commentType)
        {
            if (CommonTool.CheckNetworkStatus())
            {
                CommentDataProvider commentDataProvider = new CommentDataProvider();
                commentDataProvider.GetCommentAction(commentType);
                commentDataProvider.GetCommentCompleted += (responseData, ex) =>
                {
                    CommentResponseContent commentResponseContent = responseData as CommentResponseContent;

                    if (ex == null && commentResponseContent != null)
                    {
                        if (commentResponseContent.StatusCode == ResponseStatusCode.Success)
                        {
                            SetDataSource(commentResponseContent.ComentSource, commentType);
                        }
                        else
                            CommonTool.ShowCoding4FunToastNotify(AppResources.GetDataFailed, "");
                    }
                };
            }
            else
                CommonTool.ShowCoding4FunToastNotify(AppResources.NetworkWrong, AppResources.Login);
        }
        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LuoWangDemo.ViewModel;
using RestSharp;

namespace LuoWangDemo.Helper
{
    public class CommentDataProvider : BaseDataProvider
    {
        #region Property

        public event EventHandler GetCommentCompleted;
        #endregion

        #region Get Music Comment

        private string GetCommentRequestUrl(CommentType commentType)
        {
            return base.GetRequestUrlFromConfig(commentType);
        }

        private string HandleTitle(ContentType contentType, string origionContent)
        {
            string content = string.Empty;
            if (!string.IsNullOrEmpty(origionContent) && origionContent.Contains(" "))
            {
                switch (contentType)
                {
                    case ContentType.Title:
                        content = origionContent.Split(' ')[0];
                        break;
                    case ContentType.Comment:
                        content = origionContent.Split(' ')[1];
                        break;
                }
            }

            return content;
        }

        public void GetCommentAction(CommentType commentType)
        {
            string requestUrl = GetCommentRequestUrl(commentType);

            HttpDataRequestHelper musicCommentDataRequestHelper = new HttpDataRequestHelper();
            musicCommentDataRequestHelper.ExcuteAsyncRequest(requestUrl, Method.GET);
            musicCommentDataRequestHelper.AsyncRequestCompleted += (responseContent, ex) =>
            {
                #region Handle Response Data
                if (ex == null)
                {
                    CommentResponseContent commentResponseContent = new CommentResponseContent();
                    if (commentResponseContent.ComentSource == null)
                        commentResponseContent.ComentSource = new List<ComentModel>();

                    if (musicCommentDataRequestHelper.ResponseContent.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        #region  Data Request Success

                        try
                        {
                            #region Parse Success
                            commentResponseContent.StatusCode = ResponseStatusCode.Success;

                            XDocument commentXDocument = XDocument.Parse(responseContent.ToString(), LoadOptions.None);
                            //XElement xElement = musicCommentXDocument.Descendants("item").First();

                            //IEnumerable<ComentModel> commentModels =
                            //    from commentModel in commentXDocument.Descendants("item").Where((commentModel) => commentModel != null)
                            //    select new ComentModel()
                            //    {
                            //        CommentTitle = HandleTitle(ContentType.Title,commentModel.Descendants("title").First().Value),
                            //        CommentLinkUrl =commentModel.Descendants("link").First().Value,
                            //        ByCommentPeople = HandleTitle(ContentType.Comment,commentModel.Descendants("title").First().Value),
                            //        CommentPeople = commentModel.Descendants("dc:creator").First().Value,
                            //        CommentTime =commentModel.Descendants("pubDate").First().Value
                            //    };

                            foreach (XElement queryXElement in commentXDocument.Descendants("item"))
                            {
                                ComentModel commentModel = new ComentModel();
                                commentModel.CommentTitle = HandleTitle(ContentType.Title,
                                    queryXElement.Descendants("title").First().Value);
                                commentModel.CommentLinkUrl = queryXElement.Descendants("link").First().Value;
                                commentModel.ByCommentPeople = HandleTitle(ContentType.Comment,
                                    queryXElement.Descendants("title").First().Value); 
                                //commentModel.CommentPeople = queryXElement.Descendants("dc:creator").First().Value;
                                commentModel.CommentTime = queryXElement.Descendants("pubDate").First().Value;
                                commentResponseContent.ComentSource.Add(commentModel);
                            }


                            //if (commentModels != null)
                            //    commentResponseContent.ComentSource = commentModels.ToList();

                            #endregion

                        }
                        catch (Exception)
                        {
                            #region Parse Failed
                            commentResponseContent.StatusCode = ResponseStatusCode.Fail;
                            commentResponseContent.ErrorCode = ResponseErrorCode.ParseFailed;
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region Data Request Failed
                        commentResponseContent.StatusCode = ResponseStatusCode.Fail;
                        commentResponseContent.ErrorCode = ResponseErrorCode.DataRequestFailed;
                        #endregion
                    }

                    if (GetCommentCompleted != null)
                        GetCommentCompleted(commentResponseContent, null);

                }
                #endregion
            };
        }

        #endregion
    }
}

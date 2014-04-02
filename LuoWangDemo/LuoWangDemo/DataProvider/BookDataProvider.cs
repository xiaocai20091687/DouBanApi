using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuoWangDemo.Model;
using LuoWangDemo.ViewModel;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace LuoWangDemo.Helper
{
    public class BookDataProvider : BaseDataProvider
    {
        #region Property
        public event EventHandler GetSingleBookInforCompleted;
        #endregion

        #region Get Single Book Information

        private string GetSingleBookInforReuqestUrl()
        {
            return base.GetRequestUrlFromConfig(CommentType.SingleBookInfor);
        }

        public void GetSingleBookInforAction()
        {
            string requestUrl = GetSingleBookInforReuqestUrl();
            HttpDataRequestHelper singleBookInforDataRequestHelper = new HttpDataRequestHelper();
            singleBookInforDataRequestHelper.ExcuteAsyncRequest(requestUrl, Method.GET);
            singleBookInforDataRequestHelper.AsyncRequestCompleted += (responseContent, ex) =>
            {
                #region Handle Single Book Response
                if (ex == null)
                {
                    SingleBookInforContent singleBookInforContent = new SingleBookInforContent();

                    if (singleBookInforDataRequestHelper.ResponseContent.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        #region Data Request Success

                        try
                        {
                            #region Parse Success

                            singleBookInforContent.StatusCode = ResponseStatusCode.Success;

                            SingleBookInforModel singleBookInforModel = JsonConvert.DeserializeObject<SingleBookInforModel>(responseContent.ToString());
                            singleBookInforContent.SingleBookInforInfo = singleBookInforModel;


                            #endregion
                        }
                        catch (Exception)
                        {
                            #region Parse Failed

                            singleBookInforContent.StatusCode = ResponseStatusCode.Fail;
                            singleBookInforContent.ErrorCode = ResponseErrorCode.ParseFailed;

                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region Data Request Failed

                        singleBookInforContent.StatusCode = ResponseStatusCode.Fail;
                        singleBookInforContent.ErrorCode = ResponseErrorCode.DataRequestFailed;

                        #endregion
                    }

                    if (GetSingleBookInforCompleted != null)
                        GetSingleBookInforCompleted(singleBookInforContent,null);
                }
                #endregion
            };
        }

        #endregion
    }
}

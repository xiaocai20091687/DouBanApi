using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace LuoWangDemo.Helper
{
    public enum RequestType
    {
        GET,
        POST
    }

    public class HttpDataRequestHelper
    {
        #region Property

        public event EventHandler AsyncRequestCompleted;
        public IRestResponse ResponseContent = null;
        #endregion

        //Request WithOut File 
        public void ExcuteAsyncRequest(string requestUrl, RequestType requestType, List<KeyValuePair<string, string>> postArgumentList = null)
        {
            HttpClient requestClient = new HttpClient();
            if (requestType == RequestType.GET)
                requestClient.GetAsync(requestUrl).ContinueWith((postback) =>
                {
                    postback.Result.EnsureSuccessStatusCode();   //Return Back Status Code
                    if (AsyncRequestCompleted != null)
                        AsyncRequestCompleted(postback.Result.Content.ReadAsStringAsync().Result, null);
                });

            else if (requestType == RequestType.POST)
            {
                HttpContent content = null;
                if (postArgumentList != null)   //Get Content
                {
                    List<KeyValuePair<string, string>> argumentList = null;
                    postArgumentList.ForEach((postArgument) => { argumentList.Add(new KeyValuePair<string, string>(postArgument.Key, postArgument.Value.ToString())); });
                    content = new FormUrlEncodedContent(argumentList);
                }

                requestClient.PostAsync(requestUrl, content).ContinueWith((postback) =>
                {
                    postback.Result.EnsureSuccessStatusCode();
                    if (AsyncRequestCompleted != null)
                        AsyncRequestCompleted(postback.Result.Content.ReadAsStringAsync().Result, null);
                });
            }
        }

        //Request With File
        public void ExcuteAsyncRequest(string requestUrl, Method requestMethod, List<KeyValuePair<string, string>> postArgumentList = null, List<FileParameter> postFileList = null)
        {
            RestClient restClient = new RestClient(requestUrl);
            RestRequest restRequest = new RestRequest(requestMethod);

            if (postArgumentList != null)
                postArgumentList.ForEach((queryArgument) =>
                {
                    restRequest.AddParameter(queryArgument.Key, queryArgument.Value);
                });

            if (postFileList != null)
                postFileList.ForEach((queryFile) =>
                {
                    restRequest.Files.Add(queryFile);
                });

            restClient.ExecuteAsync(restRequest, (respontData) =>
            {
                ResponseContent = respontData;
                if (AsyncRequestCompleted != null)
                    AsyncRequestCompleted(respontData.Content, null);
            });
        }

        public void ExcuteAsyncRequestWithMedia(string requestUrl, Method requestMethod, List<KeyValuePair<string, object>> postArgumentList = null, List<FileParameter> postFileParameters = null)
        {
            RestClient restClient = new RestClient(requestUrl);
            RestRequest restRequest = new RestRequest(requestMethod);

            if (postArgumentList != null)
                postArgumentList.ForEach((queryArgument) =>
                {
                    restRequest.AddParameter(queryArgument.Key, queryArgument.Value);
                });

            if (postFileParameters != null)
                postFileParameters.ForEach((queryFile) =>
                {
                    restRequest.Files.Add(queryFile);
                });

            restClient.ExecuteAsync(restRequest, (respontData) =>
            {
                if (AsyncRequestCompleted != null)
                    AsyncRequestCompleted(respontData, null);
            });
        }
    }
}

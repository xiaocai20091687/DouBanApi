using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuoWangDemo.Helper;

namespace LuoWangDemo.ViewModel
{
    public class SingleBookInforViewModel : BaseViewModel
    {
        public void GetSingleBookInforData()
        {
            BookDataProvider bookDataProvider = new BookDataProvider();
            bookDataProvider.GetSingleBookInforAction();
            bookDataProvider.GetSingleBookInforCompleted += ((responseData, ex) =>
            {
                SingleBookInforContent singleBookInforContent = responseData as SingleBookInforContent;
                if (ex == null && singleBookInforContent != null)
                {
                    if (singleBookInforContent.StatusCode == ResponseStatusCode.Success)
                    {

                    }

                    else if (singleBookInforContent.StatusCode == ResponseStatusCode.Fail)
                    {
                        //switch (singleBookInforContent.StatusCode)
                        //{
                        //    //case "xiaocai":
                        //    //    break;
                        //    //case "xiaocai2":
                        //    //    break;
                        //}
                    }
                }
            });
        }
    }
}

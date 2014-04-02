using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuoWangDemo.Helper;
using LuoWangDemo.Model;

namespace LuoWangDemo.ViewModel
{
    public class CommentResponseContent
    {
        public ResponseStatusCode StatusCode { get; set; }
        public ResponseErrorCode ErrorCode { get; set; }

        public List<ComentModel> ComentSource { get; set; }
    }

    public class SingleBookInforContent
    {
        public ResponseStatusCode StatusCode { get; set; }

        public ResponseErrorCode ErrorCode { get; set; }

        public SingleBookInforModel SingleBookInforInfo { get; set; }
    }

    public class AccessTokenContent
    {
        public ResponseStatusCode StatusCode { get; set; }

        public ResponseErrorCode ErrorCode { get; set; }

        public AccessTokenModel AccessTokenInfo { get; set; }
    }

    public class UserInformationContent
    {
        public ResponseStatusCode StatusCode { get; set; }

        public ResponseErrorCode ErrorCode { get; set; }

        public UserInformationModel UserInformationInfo { get; set; }
    }
}

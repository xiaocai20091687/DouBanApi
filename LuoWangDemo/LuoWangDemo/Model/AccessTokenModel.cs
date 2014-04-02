using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoWangDemo.Model
{
    public class AccessTokenModel
    {
        public string AccessToken { get; set; }
        public string ExpiresIn { get; set; }

        public string RefreshToken { get; set; }

        public string DoubanUserId { get; set; }
    }
}

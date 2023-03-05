using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.Concrete
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime ExpireDate { get; set; }
        public string RefreshToken { get; set; }
    }
}

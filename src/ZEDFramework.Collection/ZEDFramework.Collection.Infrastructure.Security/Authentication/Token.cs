using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Security.Authentication
{
    public class Token
    {
        public string EncryptTokenUnity { get; set; }

        public string LoginTokenKey { get; set; }

        public string RequestTokenKey { get; set; }

        public string TokenGuid { get; set; }

        public string TokenUnity { get; set; }
    }
}

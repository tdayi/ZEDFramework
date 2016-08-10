using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.SmsSender
{
    public class SmsModel
    {
        public string Message { get; set; }

        public string Originator { get; set; }

        public string PhoneNumber { get; set; }
    }
}

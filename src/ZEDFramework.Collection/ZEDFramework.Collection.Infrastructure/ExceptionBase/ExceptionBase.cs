using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.ExceptionBase
{
    public class ExceptionBase : Exception
    {
        public readonly string[] Parameters;
        public readonly string ResponseCode;
        public readonly string[] ResponseCodes;

        public ExceptionBase()
        {
        }

        public ExceptionBase(string responseCode)
            : base(responseCode)
        {
            this.ResponseCode = responseCode;
        }

        public ExceptionBase(string[] responseCodes)
        {
            this.ResponseCodes = responseCodes;
        }

        public ExceptionBase(string responseCode, params string[] parameters)
            : base(responseCode)
        {
            this.ResponseCode = responseCode;
            this.Parameters = parameters;
        }
    }
}

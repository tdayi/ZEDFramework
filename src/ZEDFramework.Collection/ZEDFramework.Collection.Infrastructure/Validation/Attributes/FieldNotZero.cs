using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldNotZero : Attribute
    {
        internal readonly string ResponseCode;

        public FieldNotZero(
            string responseCode)
        {
            this.ResponseCode = responseCode;
        }
    }
}

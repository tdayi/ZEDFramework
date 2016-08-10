using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldLength : Attribute
    {
        internal readonly int? MaxLength;
        internal readonly int? MinLength;
        internal readonly string ResponseCode;

        public FieldLength(
            int minLength, 
            int maxLength, 
            string responseCode)
        {
            this.MinLength = new int?(minLength);
            this.MaxLength = new int?(maxLength);
            this.ResponseCode = responseCode;
        }

        public FieldLength(
            LengthType type, 
            int length, 
            string responseCode)
        {
            if (type == LengthType.Max)
            {
                this.MaxLength = new int?(length);
            }
            else
            {
                this.MinLength = new int?(length);
            }
            this.ResponseCode = responseCode;
        }
    }

    public enum LengthType
    {
        Min,
        Max
    }
}

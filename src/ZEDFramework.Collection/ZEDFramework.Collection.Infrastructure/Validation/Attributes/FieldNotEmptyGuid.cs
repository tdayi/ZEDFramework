﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldNotEmptyGuid : Attribute
    {
        internal readonly string ResponseCode;

        public FieldNotEmptyGuid(
            string responseCode)
        {
            this.ResponseCode = responseCode;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure
{
    [DataContract]
    public class ExpressionParameter
    {
        [DataMember]
        public ExpressionOperator Operator { get; set; }

        [DataMember]
        public string PropertyName { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}

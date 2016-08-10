using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Repository
{
    [DataContract]
    public class PagingRequest
    {
        [DataMember]
        public List<ExpressionParameter> FilterParameter { get; set; }

        [DataMember]
        public OrderByType? OrderByType { get; set; }

        [DataMember]
        public string OrderColumn { get; set; }

        [DataMember]
        public int? SkipCount { get; set; }

        [DataMember]
        public int? TakeCount { get; set; }
    }
}

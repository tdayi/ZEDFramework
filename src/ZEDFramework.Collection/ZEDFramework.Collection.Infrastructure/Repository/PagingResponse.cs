using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Repository
{
    [DataContract]
    public class PagingResponse<TPagingModel>
    {
        [DataMember]
        public IEnumerable<TPagingModel> Result { get; set; }

        [DataMember]
        public int TotalCount { get; set; }
    }
}

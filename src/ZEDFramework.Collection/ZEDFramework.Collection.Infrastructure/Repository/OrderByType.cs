using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Repository
{
    [DataContract]
    public enum OrderByType
    {
        [EnumMember]
        Asc = 1,

        [EnumMember]
        Desc = 2
    }
}

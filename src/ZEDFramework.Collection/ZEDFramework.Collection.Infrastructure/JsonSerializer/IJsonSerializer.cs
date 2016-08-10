using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.JsonSerializer
{
    public interface IJsonSerializer
    {
        T DeserializeObject<T>(string value);
        string SerializeObject(object value);
    }
}

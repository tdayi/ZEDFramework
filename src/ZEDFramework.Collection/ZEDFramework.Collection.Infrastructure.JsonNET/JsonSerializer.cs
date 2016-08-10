using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDFramework.Collection.Infrastructure.JsonSerializer;

namespace ZEDFramework.Collection.Infrastructure.JsonNET
{
    public class JsonSerializer : IJsonSerializer
    {
        public T DeserializeObject<T>(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }

        public string SerializeObject(object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
    }
}

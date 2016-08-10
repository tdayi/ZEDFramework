using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.XmlSerializer
{
    public interface IXmlSerializer
    {
        void SerializeToXml<T>(T obj, string fileName);
        T DeserializeFromXml<T>(string xml);
    }
}

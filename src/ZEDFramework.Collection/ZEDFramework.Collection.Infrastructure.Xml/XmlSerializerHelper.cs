using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ZEDFramework.Collection.Infrastructure.XmlSerializer;

namespace ZEDFramework.Collection.Infrastructure.Xml
{
    public class XmlSerializerHelper : IXmlSerializer
    {
        public void SerializeToXml<T>(T obj, string fileName)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    xmlSerializer.Serialize(fileStream, obj);
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public T DeserializeFromXml<T>(string xml)
        {
            T result;

            try
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(xml)))
                {
                    result = (T)xmlSerializer.Deserialize(xmlTextReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}

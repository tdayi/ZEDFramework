using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDFramework.Collection.Infrastructure.Compression;

namespace ZEDFramework.Collection.Infrastructure.IonicZip
{
    public class CompressionFactory : ICompressionFactory
    {
        public ICompression Create()
        {
            return new Compression();
        }

        public ICompression Create(string password)
        {
            return new Compression(password);
        }
    }
}

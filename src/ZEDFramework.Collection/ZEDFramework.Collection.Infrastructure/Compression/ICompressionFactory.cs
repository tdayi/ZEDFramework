using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Compression
{
    public interface ICompressionFactory
    {
        ICompression Create();
        ICompression Create(string password);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Compression
{
    public interface ICompression : IDisposable
    {
        void AddFile(string fileName);
        void AddFiles(params string[] fileNames);
        void AddDirectory(string directoryName);
        void AddDirectory(string directoryName, string directoryPathInArchive);
        void Save(string fileName);
        void Save(MemoryStream ms);
        byte[] ReadFile(string fileName);
        void ReadFile(string fileName, string unpackDirectory);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.FileTransaction
{
    public interface IFileTransaction
    {
        void CreateDirectory(string path, bool overwrite = true);
        int DeleteFile(string fileName);
        void DeleteDirectory(string path, bool checkExist = true);
        int WriteFile(string fileName, byte[] data);
        int WriteFile(string fileName, byte[] data, CreationType creationType);
    }
}

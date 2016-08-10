using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TxF;
using ZEDFramework.Collection.Infrastructure.FileTransaction;

namespace ZEDFramework.Collection.Infrastructure.TxFFileTransaction
{
    public class TxFFileTransaction : IFileTransaction
    {
        public void CreateDirectory(string path, bool overwrite = true)
        {
            Directory.CreateDirectory(path, overwrite);
        }

        public int DeleteFile(string fileName)
        {
            return File.Delete(fileName);
        }

        public void DeleteDirectory(string path, bool checkExist = true)
        {
            Directory.Delete(path, checkExist);
        }

        public int WriteFile(string fileName, byte[] data)
        {
            return File.WriteFile(File.CreateFile(fileName, File.CreationDisposition.OpensFileOrCreate), data);
        }

        public int WriteFile(string fileName, byte[] data, CreationType creationType)
        {
            return File.WriteFile(File.CreateFile(fileName,
                (creationType == CreationType.CreatesNewfileAlways) ?
                File.CreationDisposition.CreatesNewfileAlways :
                ((creationType == CreationType.CreatesNewfileIfNotExist) ?
                File.CreationDisposition.CreatesNewfileIfNotExist :
                ((creationType == CreationType.OpensFile) ?
                File.CreationDisposition.OpensFile :
                ((creationType == CreationType.OpensFileAndTruncate) ?
                File.CreationDisposition.OpensFileAndTruncate :
                File.CreationDisposition.OpensFileOrCreate)))), data);
        }
    }
}

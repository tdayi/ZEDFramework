using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDFramework.Collection.Infrastructure.Compression;

namespace ZEDFramework.Collection.Infrastructure.IonicZip
{
    public class Compression : ICompression
    {
        private ZipFile ZipFile;

        private string Password;

        public Compression()
        {
            this.ZipFile = new ZipFile();
        }

        public Compression(
            string password)
        {
            this.Password = password;
            this.ZipFile = new ZipFile();
            this.ZipFile.Password = this.Password;
        }

        public void AddFile(
            string fileName)
        {
            try
            {
                this.ZipFile.AddFile(fileName);
            }
            catch
            {
                throw;
            }
        }

        public void AddDirectory(
            string directoryName)
        {
            try
            {
                this.ZipFile.AddDirectory(directoryName);
            }
            catch
            {
                throw;
            }
        }

        public void AddDirectory(
            string directoryName,
            string directoryPathInArchive)
        {
            try
            {
                this.ZipFile.AddDirectory(directoryName, directoryPathInArchive);
            }
            catch
            {
                throw;
            }
        }

        public void AddFiles(
            params string[] fileNames)
        {
            try
            {
                this.ZipFile.AddFiles(fileNames);
            }
            catch
            {
                throw;
            }
        }

        public void Save(
            string fileName)
        {
            try
            {
                this.ZipFile.Save(fileName);
            }
            catch
            {
                throw;
            }
        }

        public void Save(
            MemoryStream ms)
        {
            try
            {
                this.ZipFile.Save(ms);
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                this.ZipFile.Dispose();
            }
            catch
            {
                throw;
            }
        }

        public byte[] ReadFile(
            string fileName)
        {
            byte[] zipData = null;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    this.ZipFile = ZipFile.Read(fileName);

                    if (!String.IsNullOrEmpty(this.Password))
                    {
                        this.ZipFile.Password = this.Password;
                    }

                    foreach (ZipEntry zip in this.ZipFile)
                    {
                        zip.Extract(ms);
                    }

                    zipData = ms.ToArray();
                }
            }
            catch
            {
                throw;
            }

            return zipData;
        }

        public void ReadFile(
            string fileName,
            string unpackDirectory)
        {
            try
            {
                this.ZipFile = ZipFile.Read(fileName);

                if (!String.IsNullOrEmpty(this.Password))
                {
                    this.ZipFile.Password = this.Password;
                }

                foreach (ZipEntry zip in this.ZipFile)
                {
                    zip.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

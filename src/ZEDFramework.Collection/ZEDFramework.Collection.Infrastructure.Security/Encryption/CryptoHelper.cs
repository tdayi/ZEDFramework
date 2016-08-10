using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Security.Encryption
{
    public class CryptoHelper
    {
        private readonly string CryptoKey;

        private readonly byte[] IV;

        public CryptoHelper()
        {
            this.IV = new byte[] { 240, 3, 0x2d, 0x1d, 0, 0x4c, 0xad, 0x3b };
            this.CryptoKey = "XCXSdFRQ!^3X??";
        }

        public CryptoHelper(string cryptoKey)
        {
            this.IV = new byte[] { 240, 3, 0x2d, 0x1d, 0, 0x4c, 0xad, 0x3b };
            this.CryptoKey = cryptoKey;
        }

        public string Decrypt(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }
            string str = string.Empty;
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(s);
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(this.CryptoKey));
                provider.IV = this.IV;
                str = Encoding.UTF8.GetString(provider.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
                throw;
            }
            return str;
        }

        public string Encrypt(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }
            string str = string.Empty;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(this.CryptoKey));
                provider.IV = this.IV;
                str = Convert.ToBase64String(provider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
            }
            catch
            {
                throw;
            }
            return str;
        }
    }
}

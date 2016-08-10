using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZEDFramework.Collection.Infrastructure.Security.Encryption;

namespace ZEDFramework.Collection.Infrastructure.Security.Authentication
{
    public static class TokenGeneration
    {
        public static Token GetToken()
        {
            Token token = new Token();
            try
            {
                token.LoginTokenKey = GetLoginTokenKey();
                token.RequestTokenKey = GetRequestTokenKey(token.LoginTokenKey);
                token.TokenGuid = Guid.NewGuid().ToString();
                token.TokenUnity = token.LoginTokenKey + "-" + token.TokenGuid + "-" + token.RequestTokenKey;
                token.EncryptTokenUnity = new CryptoHelper().Encrypt(token.TokenUnity);
            }
            catch
            {
                throw;
            }
            return token;
        }

        public static Token GetRequestTokenByClientToken(string encryptToken)
        {
            Token token = new Token();
            try
            {
                Token tokenByEncryptToken = GetTokenByEncryptToken(encryptToken);

                token.LoginTokenKey = tokenByEncryptToken.LoginTokenKey;
                token.RequestTokenKey = GetRequestTokenKey(tokenByEncryptToken.RequestTokenKey);
                token.TokenGuid = tokenByEncryptToken.TokenGuid;
                token.TokenUnity = token.LoginTokenKey + "-" + token.TokenGuid + "-" + token.RequestTokenKey;
                token.EncryptTokenUnity = new CryptoHelper().Encrypt(token.TokenUnity);
            }
            catch
            {
                throw;
            }
            return token;
        }

        public static Token GetTokenByValidateTokenRequest(string encryptToken)
        {
            Token tokenByEncryptToken = null;
            try
            {
                tokenByEncryptToken = GetTokenByEncryptToken(encryptToken);
            }
            catch
            {
                throw;
            }
            return tokenByEncryptToken;
        }

        private static string GetByTokenKey(string tokenKey)
        {
            Func<string, bool> predicate = null;
            string str = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(tokenKey))
                {
                    throw new ArgumentNullException("TokenKey IsRequired!");
                }
                List<string> tokenKeyList = GetTokenKeyList();
                if (tokenKeyList == null)
                {
                    throw new ArgumentNullException("TokenKey List Not Found!");
                }
                if (predicate == null)
                {
                    predicate = x => x.Contains(tokenKey);
                }
                str = tokenKeyList.Where<string>(predicate).FirstOrDefault<string>();
                if (string.IsNullOrEmpty(str))
                {
                    throw new ArgumentNullException("TokenKey Not Math!");
                }
            }
            catch
            {
                throw;
            }
            return str;
        }

        private static string GetLoginTokenKey()
        {
            return GetRequestTokenKey(null);
        }

        private static string GetRequestTokenKey(string requestTokenKey = null)
        {
            Func<string, bool> predicate = null;
            string str = string.Empty;
            try
            {
                List<string> tokenKeyList = GetTokenKeyList();
                if (tokenKeyList == null)
                {
                    throw new ArgumentNullException("TokenKey File List Not Found!");
                }
                if (!string.IsNullOrEmpty(requestTokenKey))
                {
                    if (predicate == null)
                    {
                        predicate = x => x.Contains(requestTokenKey);
                    }
                    string str2 = tokenKeyList.Where<string>(predicate).FirstOrDefault<string>();
                    if (string.IsNullOrEmpty(str2))
                    {
                        throw new ArgumentNullException("Deleted TokenKey Not Found!");
                    }
                    tokenKeyList.Remove(str2);
                }
                int index = new Random().Next(0, tokenKeyList.Count<string>());
                str = tokenKeyList.ToArray()[index];
            }
            catch
            {
                throw;
            }
            return str;
        }

        private static Token GetTokenByEncryptToken(string encryptToken)
        {
            Token token = new Token();
            try
            {
                if (string.IsNullOrEmpty(encryptToken))
                {
                    throw new ArgumentNullException("RequestToken IsRequired!");
                }
                string str = new CryptoHelper().Decrypt(encryptToken);
                string[] strArray = str.Split(new char[] { '-' });
                if ((strArray == null) || (strArray.Length == 0))
                {
                    throw new ArgumentException("Token Pattern Error!");
                }
                string str2 = strArray[0];
                if (string.IsNullOrWhiteSpace(str2) || string.IsNullOrWhiteSpace(GetByTokenKey(str2)))
                {
                    throw new ArgumentException("Login TokenKey Not Match!");
                }
                string str3 = strArray[strArray.Length - 1];
                if (string.IsNullOrEmpty(str3) || string.IsNullOrEmpty(GetByTokenKey(str3)))
                {
                    throw new ArgumentException("Request RokenKey Not Match!");
                }

                token.LoginTokenKey = str2;
                token.RequestTokenKey = str3;
                token.TokenGuid = str.Replace(str2 + "-", "").Replace("-" + str3, "");
                token.TokenUnity = token.LoginTokenKey + "-" + token.TokenGuid + "-" + token.RequestTokenKey;
                token.EncryptTokenUnity = new CryptoHelper().Encrypt(token.TokenUnity);
            }
            catch
            {
                throw;
            }
            return token;
        }

        private static List<string> GetTokenKeyList()
        {
            List<string> list = null;
            try
            {
                string path = ConfigurationManager.AppSettings["TokenKeyFilePath"];
                if (!File.Exists(path))
                {
                    throw new ArgumentNullException("TokenKey File Not Found!");
                }
                list = (from xml in XDocument.Load(path).Descendants("key") select xml.Value.Replace("\t", "")).ToList<string>();
            }
            catch
            {
                throw;
            }
            return list;
        }
    }
}

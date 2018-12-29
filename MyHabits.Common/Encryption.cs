using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace MyHabits.Common
{
    /// <summary>
    /// 提供加解密操作
    /// </summary>
    public static class Encryption
    {
        //DES一共就有4个参数参与运作：明文、密文、密钥、向量。为了初学者容易理解：
        //可以把4个参数的关系写成：密文=明文+密钥+向量；明文=密文-密钥-向量。
        //默认密钥向量
        private static readonly byte[] Keys = {0x19, 0x91, 0x02, 0x08, 0x92, 0xAB, 0xCD, 0xEF};

        //加密密匙or解密密匙
        private static string _encryptKey = "myHabits";

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串 </returns>
        public static string Encrypt(this string encryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(_encryptKey.Substring(0, 8)); //转换为字节
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                using (var dCSP = new DESCryptoServiceProvider()) //实例化数据加密标准
                {
                    using (var mStream = new MemoryStream()) //实例化内存流
                    {
                        using (var cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV),
                            CryptoStreamMode.Write))
                        {
                            cStream.Write(inputByteArray, 0, inputByteArray.Length); //将数据流链接到加密转换的流
                            cStream.FlushFinalBlock();
                            return Convert.ToBase64String(mStream.ToArray());
                        }
                    }
                }
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string Decrypt(this string decryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(_encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                using (var DCSP = new DESCryptoServiceProvider())
                {
                    using (var mStream = new MemoryStream())
                    {
                        using (var cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV),
                            CryptoStreamMode.Write))
                        {
                            cStream.Write(inputByteArray, 0, inputByteArray.Length);
                            cStream.FlushFinalBlock();
                            return Encoding.UTF8.GetString(mStream.ToArray());
                        }
                    }
                }
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        /// md5 16位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5_16(this string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5")
                .Substring(8, 16).ToLower();
        }

        /// <summary>
        /// md5 32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5_32(this string str)
        {
            return System.Web.Security.FormsAuthentication
                .HashPasswordForStoringInConfigFile(str, FormsAuthPasswordFormat.MD5.ToString()).ToLower();
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="toEncryptArray"></param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Decrypt_Aes(this byte[] toEncryptArray, string key)

        {
            var keyArray = Encoding.UTF8.GetBytes(key);

            var rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = rDel.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dolany.UtilityTool
{
    public static class Encryptor
    {
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="value">明文</param>
        /// <param name="key">密钥，16或32个字符</param>
        /// <param name="RijndaelIV">加密向量</param>
        /// <returns>密文</returns>
        public static string RijndaelEncrypt(string value, string key, string RijndaelIV)
        {
            using var rijAlg = Rijndael.Create();
            var encryptor = rijAlg.CreateEncryptor(Encoding.ASCII.GetBytes(key), Encoding.ASCII.GetBytes(RijndaelIV));
            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(value);
            }

            var bytesResult = msEncrypt.ToArray();

            return Convert.ToBase64String(bytesResult);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="value">密文</param>
        /// <param name="key">密钥，16或32个字符</param>
        /// <param name="RijndaelIV">加密向量</param>
        /// <returns>明文</returns>
        public static string RijndaelDecrypt(string value, string key, string RijndaelIV)
        {
            var bytesValue = Convert.FromBase64String(value);
            using var rijAlg = Rijndael.Create();
            var decryptor = rijAlg.CreateDecryptor(Encoding.ASCII.GetBytes(key), Encoding.ASCII.GetBytes(RijndaelIV));
            using var msDecrypt = new MemoryStream(bytesValue);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            var result = srDecrypt.ReadToEnd();

            return result;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">明文</param>
        /// <returns>散列后的密文</returns>
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 哈希加密
        /// </summary>
        /// <param name="str">明文</param>
        /// <returns>散列后的密文</returns>
        public static string SHA256(string str)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        /// <summary>
        /// unix时间戳
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>时间戳</returns>
        public static long TimeStamp(DateTime time)
        {
            var unixTimestamp = (long) time.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return unixTimestamp;
        }

        /// <summary>
        /// unix时间戳转日期
        /// </summary>
        /// <param name="unixTimeStamp">时间戳</param>
        /// <returns>时间</returns>
        public static DateTime TimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MinerMonitor.Utils
{
    public class Encryption
    {
        private SHA256Managed sha256Managed = new SHA256Managed();
        private RijndaelManaged aes = new RijndaelManaged();
        protected static string _password = "MinerMonitorCSharp";
        private byte[] Key { get; set; }

        public enum DesType
        {
            Encrypt = 0,
            Decrypt = 1
        }

        public Encryption()
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            Key = ASCIIEncoding.ASCII.GetBytes(_password);
        }

        // 암호화
        public byte[] AESEncrypt256(byte[] encryptData)
        {
            // Salt는 비밀번호의 길이를 SHA256 해쉬값으로 한다.
            var salt = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(_password.Length.ToString()));

            //PBKDF2(Password-Based Key Derivation Function)
            var PBKDF2Key = new Rfc2898DeriveBytes(_password, salt, 65535, HashAlgorithmName.SHA256);
            var secretKey = PBKDF2Key.GetBytes(aes.KeySize / 8);
            var iv = PBKDF2Key.GetBytes(aes.BlockSize / 8);

            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(secretKey, iv), CryptoStreamMode.Write))
                {
                    cs.Write(encryptData, 0, encryptData.Length);
                }
                xBuff = ms.ToArray();
            }
            return xBuff;
        }

        // 복호화
        public byte[] AESDecrypt256(byte[] decryptData)
        {
            var salt = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(_password.Length.ToString()));

            //PBKDF2(Password-Based Key Derivation Function)
            var PBKDF2Key = new Rfc2898DeriveBytes(_password, salt, 65535, HashAlgorithmName.SHA256);
            var secretKey = PBKDF2Key.GetBytes(aes.KeySize / 8);
            var iv = PBKDF2Key.GetBytes(aes.BlockSize / 8);

            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(secretKey, iv), CryptoStreamMode.Write))
                {
                    cs.Write(decryptData, 0, decryptData.Length);
                }
                xBuff = ms.ToArray();
            }
            return xBuff;
        }

        public string result(DesType type, string input)
        {
            var des = new DESCryptoServiceProvider()
            {
                Key = Key,
                IV = Key
            };

            var ms = new MemoryStream();

            // 익명 타입으로 transform / data 정의
            var property = new
            {
                transform = type.Equals(DesType.Encrypt) ? des.CreateEncryptor() : des.CreateDecryptor(),
                data = type.Equals(DesType.Encrypt) ? Encoding.UTF8.GetBytes(input.ToCharArray()) : Convert.FromBase64String(input)
            };

            var cryStream = new CryptoStream(ms, property.transform, CryptoStreamMode.Write);
            var data = property.data;

            cryStream.Write(data, 0, data.Length);
            cryStream.FlushFinalBlock();

            return type.Equals(DesType.Encrypt) ? Convert.ToBase64String(ms.ToArray()) : Encoding.UTF8.GetString(ms.GetBuffer());
        }
    }
}

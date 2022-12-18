using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MinerDB.DBConnect
{
    public class TripleDESCryptoService
    {
        private byte[] _IV;
        private byte[] _Key;

        public TripleDESCryptoService()
        {
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider.GenerateIV();
            cryptoServiceProvider.GenerateKey();
            this._IV = cryptoServiceProvider.IV;
            this._Key = cryptoServiceProvider.Key;
        }

        public TripleDESCryptoService(string iv, string key)
        {
            this._IV = Convert.FromBase64String(iv);
            this._Key = Convert.FromBase64String(key);
        }

        public TripleDESCryptoService(byte[] iv, byte[] key)
        {
            this._IV = iv;
            this._Key = key;
        }

        public string IV => Convert.ToBase64String(this._IV);

        public string Key => Convert.ToBase64String(this._Key);

        public string Encrypt(string plainText)
        {
            string empty = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            DES des = (DES)new DESCryptoServiceProvider();
            des.Mode = CipherMode.CBC;
            string s1 = "MINER__K";
            string s2 = "MINER_IV";
            des.Key = Encoding.UTF8.GetBytes(s1);
            des.IV = Encoding.UTF8.GetBytes(s2);
            ICryptoTransform encryptor = des.CreateEncryptor();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] array = memoryStream.ToArray();
            memoryStream.Close();
            memoryStream.Dispose();
            return Convert.ToBase64String(array, 0, array.Length);
        }

        public string Decrypt(string verifyCode)
        {
            byte[] buffer = Convert.FromBase64String(verifyCode);
            DES des = (DES)new DESCryptoServiceProvider();
            des.Mode = CipherMode.CBC;
            string s1 = "MINER__K";
            string s2 = "MINER_IV";
            des.Key = Encoding.UTF8.GetBytes(s1);
            des.IV = Encoding.UTF8.GetBytes(s2);
            ICryptoTransform decryptor = des.CreateDecryptor();
            StreamReader streamReader = new StreamReader((Stream)new CryptoStream((Stream)new MemoryStream(buffer, 0, buffer.Length), decryptor, CryptoStreamMode.Read));
            string end = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();
            return end;
        }

        public string Encode(string str, Encoding en) => Convert.ToBase64String(this.EncodeToByte(str, en));

        public byte[] EncodeToByte(string str, Encoding en)
        {
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(this._Key, this._IV), CryptoStreamMode.Write);
            byte[] bytes = en.GetBytes(str);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] array = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return array;
        }

        public string Decode(string str, Encoding en) => this.Decode(Convert.FromBase64String(str), en);

        public string Decode(byte[] encrypted, Encoding en)
        {
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(this._Key, this._IV), CryptoStreamMode.Write);
            cryptoStream.Write(encrypted, 0, encrypted.Length);
            cryptoStream.Close();
            byte[] array = memoryStream.ToArray();
            memoryStream.Close();
            return en.GetString(array, 0, array.Length);
        }
    }
}

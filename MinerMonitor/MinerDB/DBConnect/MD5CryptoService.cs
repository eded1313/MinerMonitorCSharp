using System;
using System.Security.Cryptography;
using System.Text;

namespace MDB.DBConnect
{
    public class MD5CryptoService
    {
        public static string GetMD5(string str, Encoding en) => Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(en.GetBytes(str)));
    }
}

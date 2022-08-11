using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Net;

/// <summary>
/// SymmetricKeyEncrypt의 요약 설명입니다.
/// </summary>
public class SymmetricKeyEncrypt
{
    private byte[] Key { get; set; }
    private SHA256Managed sha256Managed = new SHA256Managed();
    private RijndaelManaged aes = new RijndaelManaged();
    //var salt = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(password.Length.ToString()));
    //var PBKDF2Key = new Rfc2898DeriveBytes(password, salt, 65535);
    private byte[] salt { get; set; }
    private Rfc2898DeriveBytes PBKDF2Key;

    public string url = string.Empty;
    private static byte[] rijnKey = null;
    private static byte[] rijnIV = Encoding.UTF8.GetBytes("lotusminermonitors");

	public SymmetricKeyEncrypt(EncryptType type, string key)
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
        if(type == EncryptType.DES) {
            Key = ASCIIEncoding.ASCII.GetBytes(key);
        }
        else if (type == EncryptType.SECURITY_AES256)
        {
            salt = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(key.Length.ToString()));
            PBKDF2Key = new Rfc2898DeriveBytes(key, salt, 65535);

            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = PBKDF2Key.GetBytes(aes.KeySize / 8); // 32byte
            aes.IV = PBKDF2Key.GetBytes(aes.BlockSize / 8); // 16byte
        }
        else if (type == EncryptType.FAST_AES256)
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }
        else if (type == EncryptType.ENCRYPTIT) {
            rijnKey = Encoding.UTF8.GetBytes(key);
        }
	}

    public enum EncryptType {
        DES,
        SECURITY_AES256,
        FAST_AES256,
        ENCRYPTIT
    }

    public enum DesType {
        Encrypt,
        Decrypt,
    }

    #region DES Encrypt
    // Key 값은 무조건 8자리
    public string EncryptResult(DesType type, string input)
    {
        var des = new DESCryptoServiceProvider() { 
            Key = Key,
            IV = Key
        };

        MemoryStream ms = new MemoryStream();
 
        var property = new {
            transform = type.Equals(DesType.Encrypt) ? des.CreateEncryptor() : des.CreateDecryptor(),
            data = type.Equals(DesType.Encrypt) ?  Encoding.UTF8.GetBytes(input.ToCharArray()) : Convert.FromBase64String(input)
        };

        CryptoStream cryStream = new CryptoStream(ms, property.transform, CryptoStreamMode.Write);

        var data = property.data;
 
        cryStream.Write(data, 0, data.Length);
        cryStream.FlushFinalBlock();
 
        return type.Equals(DesType.Encrypt) ? Convert.ToBase64String(ms.ToArray()) : Encoding.UTF8.GetString(ms.GetBuffer());
    }
    #endregion

    #region AES - ECB Encrypt
    public static String EncryptIt(String s)
    {
        String result;
        RijndaelManaged rijn = new RijndaelManaged();
        rijn.Mode = CipherMode.ECB;
        rijn.Padding = PaddingMode.Zeros; // php와의 연동에서 꼭 확인
        rijn.BlockSize = 256;

        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (ICryptoTransform encryptor = rijn.CreateEncryptor(rijnKey, rijnIV))
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(s);
                    }
                }
            }
            result = Convert.ToBase64String(msEncrypt.ToArray());
        }
        rijn.Clear();

        result = Base64UrlEncode(result);

        return result;
    }

    public static String DecryptIt(String s)
    {
        String result;
        RijndaelManaged rijn = new RijndaelManaged();
        rijn.Mode = CipherMode.ECB;
        rijn.Padding = PaddingMode.Zeros;
        rijn.BlockSize = 256;

        s = Base64UrlDecode(s);

        using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(s)))
        {
            using (ICryptoTransform decryptor = rijn.CreateDecryptor(rijnKey, rijnIV))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader swDecrypt = new StreamReader(csDecrypt))
                    {
                        result = swDecrypt.ReadToEnd();
                    }
                }
            }
        }
        rijn.Clear();

        return result;
    }
    #endregion

    #region AES256 - CBC Encrypt
    public String AES256(DesType Type, String Input)
    {
        String Output;
        if (Type == DesType.Encrypt)
        {
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(Input);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = ms.ToArray();
            }

            Output = Convert.ToBase64String(xBuff);
        }
        else
        {
            //var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            var decrypt = aes.CreateDecryptor();

            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(Input);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = ms.ToArray();
            }

            Output = Encoding.UTF8.GetString(xBuff);
        }

        return Output;
    }
    #endregion

    public static string Base64UrlEncode(string arg)
    {
        arg = arg.Split('=')[0]; // Remove any trailing '='s
        arg = arg.Replace('+', '-'); // 62nd char of encoding
        arg = arg.Replace('/', '_'); // 63rd char of encoding
        return arg;
    }

    public static string Base64UrlDecode(string arg)
    {
        string s = arg;
        s = s.Replace('-', '+'); // 62nd char of encoding
        s = s.Replace('_', '/'); // 63rd char of encoding
        switch (s.Length % 4) // Pad with trailing '='s
        {
            case 0: break; // No pad chars in this case
            case 2: s += "=="; break; // Two pad chars
            case 3: s += "="; break; // One pad char
            default:
                throw new System.Exception("Illegal base64url string!");
        }
        return s;
    }
}
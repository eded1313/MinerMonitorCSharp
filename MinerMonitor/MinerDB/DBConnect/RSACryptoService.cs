using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace MDB.DBConnect
{
    public class RSACryptoService
    {
        private static int rsa_EncChunkSize = 50;

        public static string EncryptRSA(byte[] byteText, string szPubKey)
        {
            int num1 = 128;
            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
            cryptoServiceProvider.FromXmlString(szPubKey);
            int srcOffset = 0;
            ArrayList arrayList = new ArrayList();
            int length = 0;
            int count = RSACryptoService.rsa_EncChunkSize;
            int num2 = 0;
            while (true)
            {
                int num3 = byteText.Length - srcOffset;
                if (num3 > 0)
                {
                    if (num3 < RSACryptoService.rsa_EncChunkSize)
                        count = num3;
                    ++num2;
                    byte[] rgb = new byte[count];
                    Buffer.BlockCopy((Array)byteText, srcOffset, (Array)rgb, 0, count);
                    byte[] numArray = cryptoServiceProvider.Encrypt(rgb, false);
                    num1 = numArray.Length;
                    length += numArray.Length;
                    arrayList.Add((object)numArray);
                    srcOffset += RSACryptoService.rsa_EncChunkSize;
                }
                else
                    break;
            }
            byte[] inArray = new byte[length];
            for (int index = 0; index < arrayList.Count; ++index)
            {
                byte[] numArray = (byte[])arrayList[index];
                Buffer.BlockCopy((Array)numArray, 0, (Array)inArray, index * num1, numArray.Length);
            }
            return Convert.ToBase64String(inArray);
        }

        public static byte[] DecryptRSA(string strEncText, string szPrvKey, int decChunkSize)
        {
            if (strEncText == "" || szPrvKey == "")
                return (byte[])null;
            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
            cryptoServiceProvider.FromXmlString(szPrvKey);
            byte[] numArray1 = Convert.FromBase64String(strEncText);
            ArrayList arrayList = new ArrayList();
            int srcOffset = 0;
            int count = decChunkSize;
            int length = 0;
            while (true)
            {
                int num = numArray1.Length - srcOffset;
                if (num > 0)
                {
                    if (num < decChunkSize)
                        count = num;
                    byte[] rgb = new byte[count];
                    Buffer.BlockCopy((Array)numArray1, srcOffset, (Array)rgb, 0, count);
                    byte[] bytes = cryptoServiceProvider.Decrypt(rgb, false);
                    length += bytes.Length;
                    Encoding.UTF8.GetString(bytes);
                    arrayList.Add((object)bytes);
                    srcOffset += decChunkSize;
                }
                else
                    break;
            }
            byte[] numArray2 = new byte[length];
            for (int index = 0; index < arrayList.Count; ++index)
            {
                byte[] numArray3 = (byte[])arrayList[index];
                Buffer.BlockCopy((Array)numArray3, 0, (Array)numArray2, index * RSACryptoService.rsa_EncChunkSize, numArray3.Length);
            }
            return numArray2;
        }
    }
}

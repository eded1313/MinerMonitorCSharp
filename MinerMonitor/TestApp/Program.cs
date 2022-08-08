using MinerMonitor.Utils;
using System;
using System.Text;
using static MinerMonitor.Utils.Encryption;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Encryption aes = new Encryption();
            var planeText = "192.168.10.128,22,jhseo,wjdgns0516";
            var key = "lotusminermonitoring1234";

            SymmetricKeyEncrypt encrypt = new SymmetricKeyEncrypt(SymmetricKeyEncrypt.EncryptType.FAST_AES256, key);
            string value = encrypt.AES256(SymmetricKeyEncrypt.DesType.Encrypt, planeText);

            Console.WriteLine("aes 인크립트 된 문자열 : " + value);

            string refvalue = encrypt.AES256(SymmetricKeyEncrypt.DesType.Decrypt, value);
            Console.WriteLine("디크립트 된 문자열 : " + refvalue);

            //Console.WriteLine("오리지널 문장 : " + planeText);
            //Console.WriteLine("대칭키로 쓰일 키 : " + password);
            //Console.WriteLine("");

            ////스트링을 byte배열로 변환
            //var byteArray = Encoding.UTF8.GetBytes(planeText);

            ////AES256으로 인크립트
            //byte[] encryptedArray = aes.AESEncrypt256(byteArray);
            //Console.WriteLine("인크립트 된 문자열 : " + Convert.ToBase64String(encryptedArray));

            //string test = Convert.ToBase64String(encryptedArray);
            //byte[] testnb = Convert.FromBase64String(test);

            ////디크립트(AES256)
            //byte[] decryptedArray = aes.AESDecrypt256(testnb);
            //var decryptedString = Encoding.UTF8.GetString(decryptedArray);
            //Console.WriteLine("디크립트 된 문자열 : " + decryptedString);
            //Console.WriteLine("");

            //string value = aes.result(DesType.Encrypt, planeText);

            //Console.WriteLine("aes 인크립트 된 문자열 : " + value);

            //string res = aes.result(DesType.Decrypt, value);

            //Console.WriteLine("aes 디크립트 된 문자열 : " + res);
        }
    }
}

using MinerMonitor.Connect;
using MinerMonitor.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace MinerMonitor
{
    class Program
    {
        protected static string key = "lotusminermonitoring1234";

        static async Task Main(string[] args)
        {
            try
            {
                Setting setting = new Setting();
                string[] serverInfo = setting.GetServerInfo();

                #region Test Code
                SymmetricKeyEncrypt encrypt = new SymmetricKeyEncrypt(SymmetricKeyEncrypt.EncryptType.FAST_AES256, key);
                string server = encrypt.AES256(SymmetricKeyEncrypt.DesType.Decrypt, serverInfo[0]);

                string host = server.Split(",")[0].ToString();
                int port = Convert.ToInt32(server.Split(",")[1].ToString());
                string username = server.Split(",")[2].ToString();
                string passwd = server.Split(",")[3].ToString();


                SSHConnection connection = new SSHConnection(host, port, username, passwd, "Golden 1호");

                if (!await connection.Connect())
                {
                    Console.WriteLine("Connection Fail");
                    return;
                }
                #endregion

                #region Origin Code
                //foreach (var data in serverInfo)
                //{
                //    SymmetricKeyEncrypt encrypt = new SymmetricKeyEncrypt(SymmetricKeyEncrypt.EncryptType.FAST_AES256, key);
                //    string server = encrypt.AES256(SymmetricKeyEncrypt.DesType.Decrypt, data);

                //    string host = server.Split(",")[0].ToString();
                //    int port = Convert.ToInt32(server.Split(",")[1].ToString());
                //    string username = server.Split(",")[2].ToString();
                //    string passwd = server.Split(",")[3].ToString();


                //    SSHConnection connection = new SSHConnection(host, port, username, passwd);

                //    if (!connection.Connect())
                //    {
                //        Console.WriteLine("Connection Fail");
                //        return;
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}

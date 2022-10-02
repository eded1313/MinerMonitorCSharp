using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using static MinerMonitor.Utils.Encryption;

namespace MinerMonitor.Utils
{
    public class Setting
    {
        public Dictionary<string, object> ConnectionInfo => _connectionInfo;
        private Dictionary<string, object> _connectionInfo = new Dictionary<string, object>();

        public Setting()
        {

        }

        /// <summary>
        /// SSH Server 정보 가져오기
        /// </summary>
        public string[] GetServerInfo()
        {
            string[] result;
            //상대경로//
            string path = @".\server\server.txt";
            result = File.ReadAllLines(path);



            return result;
        }

        public ConnectionInfo GetConnectionInfo(string host, int port, string username, string password)
        {
            ConnectionInfo ConnNfo = new ConnectionInfo(host, port, username,
                new AuthenticationMethod[]{
                    new PasswordAuthenticationMethod(username, password),
                }
            );

            return ConnNfo;
        }
    }
}

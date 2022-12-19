using MinerDaemon.Enum;
using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using static Miner.Helper.Setting;

namespace Miner.Helper
{
    public static class Setting
    {
        /// <summary>
        /// SSH Server 정보 가져오기
        /// </summary>
        public static string[] GetServerInfo()
        {
            string[] result;
            //상대경로//
            string path = GetAbsolutePath() + @"\server\server.txt";
            result = File.ReadAllLines(path);

            return result;
        }

        public static string GetAbsolutePath()
        {
            return new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        }

        public static ConnectionInfo GetConnectionInfo(string host, int port, string username, string password)
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

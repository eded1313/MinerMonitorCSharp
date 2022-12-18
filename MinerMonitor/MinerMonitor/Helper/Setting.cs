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
        private static string SLACK_WEBHOOK = "https://hooks.slack.com/services/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";
        private static string TEST_SLACK_WEBHOOK = "https://hooks.slack.com/services/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";
        private static string NASMG_SLACK_WEBHOOK = "https://hooks.slack.com/services/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";

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

        public static string GetSlackChannel() => SLACK_WEBHOOK;
        public static string TestChannel() => TEST_SLACK_WEBHOOK;
        public static string NasmgChannel() => NASMG_SLACK_WEBHOOK;

        public static string GetSlackMsgColor(ResultType type)
        {
            string color = string.Empty;
            switch (type)
            {
                case ResultType.PRIMARY:
                    color = "#007bff";
                    break;
                case ResultType.INFO:
                    color = "#17a2b8";
                    break;
                case ResultType.SUCCESS:
                    color = "#28a745";
                    break;
                case ResultType.WARNING:
                    color = "#ffc107";
                    break;
                case ResultType.DANGER:
                    color = "#dc3545";
                    break;
            }
            return color;
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

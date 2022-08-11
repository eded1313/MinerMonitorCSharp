﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MinerMonitor.Utils;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace MinerMonitor.Connect
{
    public class SSHConnection : IDisposable
    {
        private SshClient client;
        public IReadOnlyDictionary<string, object> ServerInfo => _serverInfo;
        private Dictionary<string, object> _serverInfo = new Dictionary<string, object>();
        private ConnectionInfo info;
        public string Device => _deviceName;
        private string _deviceName;

        public SSHConnection(string host, int port, string username, string password, string devicename)
        {
            Setting setting = new Setting();
            info = setting.GetConnectionInfo(host, port, username, password);
            _deviceName = devicename;
        }

        public async Task<bool> Connect()
        {
            try
            {

                client = new SshClient(info);
                client.Connect();
                Miner.Miner miner = new Miner.Miner(client);
                if (! await miner.ExcuteTaskAsync())
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                client.Disconnect();
            }

            return true;
        }

        public void Dispose()
        {
            client.Disconnect();
        }
    }
}

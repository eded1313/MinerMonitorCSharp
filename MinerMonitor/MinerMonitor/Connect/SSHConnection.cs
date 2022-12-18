using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;
using Miner.Helper;
using MinerMonitor.Utils;
using MinerDaemon.Helper;
using MinerDaemon.Enum;

namespace MinerMonitor.Connect
{
    public class SSHConnection:IDisposable
    {
        private SshClient client;
        public IReadOnlyDictionary<string, object> ServerInfo => _serverInfo;
        private Dictionary<string, object> _serverInfo = new Dictionary<string, object>();
        private ConnectionInfo info;
        public string Device => _deviceName;
        private string _deviceName;
        ILogger _logger = new Logger();


        public SSHConnection(string host, int port, string username, string password, string devicename)
        {
            info = Setting.GetConnectionInfo(host, port, username, password);
            _deviceName = devicename;
        }

        public async Task<bool> Connect()
        {
            try
            {
                client = new SshClient(info);
                client.Connect();
                Miner.Miner miner = new Miner.Miner(client, _deviceName);
                if (!await miner.ExecuteTaskAsync())
                {
                    return false;
                }
                    
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                string errMsg = "SSH connection to " + _deviceName + " server failed" + Environment.NewLine + "Check the log for more details";
                var errOption = new SlackMessageOptionModel {color = Setting.GetSlackMsgColor(ResultType.DANGER), text = errMsg, title = "[Test] SSH Connection Fail!" };
                if (!await SendMessageAsync(errOption))
                {
                    _logger.Error("using slack bot send message fail");
                }

                return false;
            }
            finally
            {
                client.Disconnect();
            }

            return true;
        }

        private async Task<bool> SendMessageAsync(SlackMessageOptionModel message)
        {
            try
            {
                SlackClient client = new SlackClient(Setting.TestChannel());
                if (!await client.SendMessageAsync(message))
                {
                    _logger.Info("Fail Send Message");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }

        public void Dispose()
        {
            client.Disconnect();
        }
    }
}

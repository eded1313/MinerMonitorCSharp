using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;
using Miner.Helper;
using MinerDaemon.Helper;
using MinerDaemon.Enum;
using MinerSlack;

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
                var errOption = SlackOption.MakeSlackOption(SlackOption.GetSlackMsgColor(ResultType.DANGER), "[Test] SSH Connection Fail!", errMsg);

                SlackAPI slack = new SlackAPI(SlackChannel.TEST_SLACK_WEBHOOK);
                if (!await slack.ExecuteAsync(errOption))
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

        public void Dispose()
        {
            client.Disconnect();
        }
    }
}

using MinerMonitor.Utils;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MinerMonitor.Miner
{
    public interface IMinerCommand
    {
        List<string> CommandString { get; }
        void AddCommandString(string command);
    }

    public class Miner : IMinerCommand
    {
        private List<string> _commandString = new List<string>();
        public List<string> CommandString => _commandString;
        private SshClient _client;

        protected const string WORKER_STATE_CMD = "lotus-miner sealing workers | grep host";
        protected const string GPU_STATE_CMD = "nvidia-smi | grep Version";
        protected const string DISK_STATE_CMD = "df -h | grep lotus";
        protected const string SYNC_STATE_CMD = "lotus sync wait | grep Done";
        protected const string SLACK_WEBHOOK = "https://hooks.slack.com/services/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";

        public Miner(SshClient Client)
        {
            _client = Client;
        }

        public void AddCommandString(string command)
        {
            _commandString.Add(command);
        }

        private string GetCommandLine(string command)
        {
            string cmd = _client.CreateCommand(command).Execute();
            return cmd;
        }

        public async Task<bool> ExcuteTask()
        {
            //AddCommandString(WORKER_STATE_CMD);
            AddCommandString(GPU_STATE_CMD);
            AddCommandString(DISK_STATE_CMD);
            AddCommandString(SYNC_STATE_CMD);

            if (!await WriteMinerLog())
                return false;

            return true;
        }

        private async Task<bool> WriteMinerLog()
        {
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string filePath = @"./server/"+ currentDate + "_minerLog.txt";
            List<string> lines = new List<string>();

            try
            {
                foreach (var command in _commandString)
                {
                    string result = GetCommandLine(command);
                    Console.WriteLine("===========================================");
                    Console.WriteLine(result);
                    lines.Add(result);
                }

                string text = string.Empty;

                foreach (var line in lines)
                {
                    text += line + Environment.NewLine;
                }
                text += "===========================================" + Environment.NewLine;

                await File.AppendAllTextAsync(filePath, text);

                SlackClient client = new SlackClient(SLACK_WEBHOOK);

                if (!await client.SendMessageAsync(text))
                {
                    Console.WriteLine("Fail Send Message");
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return true;
        }
    }
}

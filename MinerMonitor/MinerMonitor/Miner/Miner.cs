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
            //AddCommandString(GPU_STATE_CMD);
            AddCommandString(DISK_STATE_CMD);
            AddCommandString(SYNC_STATE_CMD);

            if (!await MinerLogAsync())
                return false;

            return true;
        }

        private async Task<bool> MinerLogAsync()
        {
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string folderpath = @"./MinerLog";
            string filePath = folderpath + "/" + currentDate + "_minerLog.txt";
            string logText = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]" + Environment.NewLine;
            string message = "##################################################" + Environment.NewLine;

            CheckBeforeFile();
            CheckFileExist(folderpath, filePath);

            try
            {
                foreach (var command in _commandString)
                {
                    string result = GetCommandLine(command);
                    Console.WriteLine("===========================================");
                    Console.WriteLine(result);

                    logText += result + Environment.NewLine;
                    
                    ConvertMessage(result, command, ref message);
                }

                logText += "===========================================" + Environment.NewLine;
                await File.AppendAllTextAsync(filePath, logText);

                message += Environment.NewLine + "##################################################";

                if (!await SendMessageAsync(message))
                {
                    Console.WriteLine("using slack bot send message fail");
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return true;
        }

        private void CheckBeforeFile()
        {
            string beforeDate = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string filePath = @"./MinerLog/" + beforeDate + "_minerLog.txt";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return;
            }
        }

        private void CheckFileExist(string folder, string file)
        {
            DirectoryInfo di = new DirectoryInfo(folder);

            if (!di.Exists)
                di.Create();

            if (!File.Exists(file))
                File.Create(file);
        }

        private async Task<bool> SendMessageAsync(string message)
        {
            try
            {
                SlackClient client = new SlackClient(SLACK_WEBHOOK);
                if (!await client.SendMessageAsync(message))
                {
                    Console.WriteLine("Fail Send Message");
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void ConvertMessage(string text, string command, ref string message)
        {
            try
            {
                string msg = string.Empty;

                if (command.Equals(DISK_STATE_CMD))
                {
                    string[] dfResult = text.Split("\n");

                    foreach (var res in dfResult)
                    {
                        int per = 0;
                        if (!string.IsNullOrEmpty(res))
                        {
                            string dfr = res.Substring(res.IndexOf("%") - 2, 2).TrimStart();
                            per = Convert.ToInt32(dfr);
                        }

                        if (per > 70)
                        {
                            message += res + "\n";
                        }
                    }
                }
                else if (command.Equals(SYNC_STATE_CMD))
                {
                    if (text.Equals("Done!\n"))
                    {
                        message += "OK!";
                    }
                }
                else
                {
                    message += text;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

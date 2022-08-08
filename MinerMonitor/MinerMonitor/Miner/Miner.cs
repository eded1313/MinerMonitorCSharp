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

        public bool ExcuteTask()
        {
            //AddCommandString(WORKER_STATE_CMD);
            AddCommandString(GPU_STATE_CMD);
            AddCommandString(DISK_STATE_CMD);
            AddCommandString(SYNC_STATE_CMD);

            foreach (var command in _commandString)
            {
                string result = GetCommandLine(command);
                try
                {
                    Console.WriteLine("===========================================");
                    Console.WriteLine(result);
                    List<string> lines = new List<string>();
                    lines.Add(result);

                    using (StreamWriter outputFile = new StreamWriter(@"C:\Users\서정훈\Desktop\Work\Miner-CSharp\MinerMonitor\MinerMonitor\bin\Debug\netcoreapp3.1\server\miner_log.txt"))
                    {
                        foreach (var line in lines)
                        {
                            outputFile.WriteLine(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return true;
        }
    }
}

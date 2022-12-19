using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using MinerDaemon.Helper;
using System.Reflection;
using Miner.Helper;
using static Miner.Helper.Setting;
using MinerDaemon.Enum;
using MinerSlack;

namespace MinerMonitor.Miner
{
    public class Miner
    {
        ILogger _logger = new Logger();

        private List<string> _commandString = new List<string>();
        private readonly SshClient _client;
        private readonly string _deviceName;
        
        public Miner(SshClient Client, string DeviceName)
        {
            _client = Client;
            _deviceName = DeviceName;
        }

        private void AddCommandString(string command) => _commandString.Add(command);

        private string GetCommandLine(string command) => _client.CreateCommand(command).Execute();

        public async Task<bool> ExecuteTaskAsync()
        {
            string currentHour = DateTime.Now.ToString("HH");
            foreach (var pair in CommandHelper.CommandExeTime)
            {
                if (pair.Key == ExecuteType.ONCE)
                {
                    if (currentHour.Equals("10"))
                        pair.Value.ForEach(x => AddCommandString(CommandHelper.MonitoringCommand[x]));
                }
                else
                {
                    pair.Value.ForEach(x => AddCommandString(CommandHelper.MonitoringCommand[x]));
                }
            }

            if (!await MinerLogAsync())
                return false;

            return true;
        }

        private async Task<bool> MinerLogAsync()
        {
            string logText = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " => "+ _deviceName + "]" + Environment.NewLine;
            string message = "##################################################" + Environment.NewLine;
            string resultText = string.Empty;
            ResultType result = ResultType.SUCCESS;

            CheckBeforeFile();

            try
            {
                foreach (var command in _commandString)
                {
                    string executeResult = GetCommandLine(command);
                    string refCommand = CommandHelper.MonitoringCommand.FindKeyByValue(command).ToString();

                    if (string.IsNullOrEmpty(refCommand))
                    {
                        _logger.Error("Invaild Command Extraction");
                        return false;
                    }

                    logText += "Commnad => " + refCommand + Environment.NewLine + executeResult + Environment.NewLine;

                    if (!ConvertMessage(executeResult, command, ref resultText))
                        result = ResultType.DANGER;
                }

                logText += "===========================================" + Environment.NewLine;

                _logger.Info(logText);
                message += resultText + Environment.NewLine + "##################################################";

                /// 모든 명령어의 실행 결과가 정상일 때 slack message 보내지 않음
                //if (string.IsNullOrEmpty(resultText))
                //    return true;

                var msgOption = SlackOption.MakeSlackOption(SlackOption.GetSlackMsgColor(result), "Miner Log Device: " + _deviceName, message);
                SlackAPI slack = new SlackAPI(SlackChannel.TEST_SLACK_WEBHOOK);
                if (!await slack.ExecuteAsync(msgOption))
                {
                    _logger.Error("using slack bot send message fail");
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }

            return true;
        }

        static async Task WriteFileAsync(string file, string content)
        {
            Console.WriteLine("Async Write File has started");
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(file)))
            {
                await outputFile.WriteAsync(content);
            }
            Console.WriteLine("Async Write File has completed");
        }

        private void CheckBeforeFile()
        {
            string beforeDate = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string exeLogFilePath = Setting.GetAbsolutePath() + @"/logs/log.txt_" + beforeDate + ".txt";
            string errorLogFilePath = Setting.GetAbsolutePath() + @"/logs/error.txt_" + beforeDate + ".txt";

            // 일주일 전 실행 Log Delete
            if (File.Exists(exeLogFilePath))
            {
                File.Delete(exeLogFilePath);
            }

            // 일주일 전 Error Log Delete
            if (File.Exists(errorLogFilePath))
            {
                File.Delete(errorLogFilePath);
            }

            return;
        }

        private void CheckFileExist(string folder, string file)
        {
            DirectoryInfo di = new DirectoryInfo(folder);

            if (!di.Exists)
                di.Create();

            if (!File.Exists(file))
                File.Create(file);
        }

        private bool ConvertMessage(string text, string command, ref string message)
        {
            try
            {
                switch (CommandHelper.MonitoringCommand.FindKeyByValue(command))
                {
                    case CommandEnum.DISK_STATE_CMD:
                        {
                            string[] dfResult = text.Split('\n');
                            bool diskFlag = true;
                            foreach (var res in dfResult)
                            {
                                int per = 0;
                                if (!string.IsNullOrEmpty(res))
                                {
                                    string dfr = res.Substring(res.IndexOf("%") - 2, 2).TrimStart();
                                    per = Convert.ToInt32(dfr);
                                }

                                if (per > 90)
                                {
                                    message += res + "\n";
                                    diskFlag = false;
                                }
                            }
                            return diskFlag;
                        }
                    case CommandEnum.SYNC_STATE_CMD:
                        {
                            if (text.Equals("Done!\n"))
                            {
                                // SYNC_STATE_CMD 실행 결과가 정상일 경우 string.empty return
                                message += "Sync OK!\n";
                                //message += string.Empty;
                                return true;
                            }
                            else
                            {
                                message += "Sync Fail!";
                                return false;
                            }
                        }
                    case CommandEnum.WORKER_STATE_CMD:
                        {
                            if (string.IsNullOrEmpty(text)) { return true; }
                            else { message += text; return false; }

                        }
                    case CommandEnum.GPU_STATE_CMD:
                        {
                            message += text;
                            return true;
                        }
                    case CommandEnum.SOCAT_STATE_UNKNOWN_CMD:
                        {
                            if (string.IsNullOrEmpty(text)) { return true; }
                            else { message += text; return false; }
                        }
                    case CommandEnum.CHECK_EXPIRE:
                        {
                            if (text != "1\n")
                            {
                                message += "CHECK_EXPIRE: " + text;
                                return false;
                            }
                            else
                                return true;
                        }
                    case CommandEnum.SECTOR_FAULT:
                        {
                            if (string.IsNullOrEmpty(text))
                            {
                                return true;
                            }
                            else
                            {
                                message += "SECTOR_FAULT: " + text;
                                return false;
                            }
                        }
                    default:
                        message += text;
                        return false;
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }
    }
}

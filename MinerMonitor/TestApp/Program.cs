using MDB;
using MDB.DBObject;
using MinerMonitor.Helper;
using MinerSlack;
using System;
using System.Linq;
using System.Text;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DBExecutor executor = new DBExecutor(DBConnType.MinerServer);
            executor.ProcedureInitialize();

            var spGetDevice = new SpetServerInfo(MDB.SQLDB.MSSQL.Startup.DBName.Miner, 0, null);
            if (!spGetDevice.Execute())
            {
                return;
            }
            else
            { 

            }

            var list = spGetDevice.DeviceList.Select(x => Convert.ToInt32(x.Device.Substring(5))).ToList();

            string deviceId = "miner" + RandomDevice.GetRandomID(list);

            string host = "115.94.18.238";
            int port = 33338;
            string user = "ed";
            string passwd = "999vpxk2@";
            string deviceName = "게임";

            DeviceRow row = new DeviceRow(deviceId, host, port, user, passwd, deviceName, true);

            executor.ExecuteQuery(row);
            //executor.ExecuteDatabaseSync(row);


            //var msgOption = SlackOption.MakeSlackOption(SlackOption.GetSlackMsgColor(ResultType.SUCCESS), "Miner Log Device: " + deviceName, "TesttestTesttest");
            //SlackAPI slack = new SlackAPI(SlackChannel.TEST_SLACK_WEBHOOK);
            //if (!await slack.ExecuteAsync(msgOption))
            //{
            //    Console.WriteLine("using slack bot send message fail");
            //    return;
            //}
        }
    }
}

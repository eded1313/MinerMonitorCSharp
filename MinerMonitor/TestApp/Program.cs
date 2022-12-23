using MDatabase;
using MinerDB.RowObject;
using MinerMonitor.Helper;
using System;
using System.Text;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DBExecutor executor = new DBExecutor(DBConnType.MinerServer);

            string deviceId = "miner" + RandomDevice.GetRandomID();

            string host = "115.94.18.238";
            int port = 33338;
            string user = "ed";
            string passwd = "999vpxk2@";
            string deviceName = "게임";

            DeviceRow row = new DeviceRow(deviceId, host, port, user, passwd, deviceName, true);

            executor.ExecuteQuery(row);
        }
    }
}

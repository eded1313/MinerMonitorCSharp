using MinerDB.Dac;
using MinerDB.DBConnect;
using MinerDB.RowObject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TestDatabase;
using TestDatabase.SQLDB;
using TestDatabase.SQLDB.MSSQL;

namespace MinerDB
{
    class Program
    {
        static void Main(string[] args)
        {
            // 실제 Miner DB Connection 암호화 문자열
            string connectString = "d7Pt1WHH9OZCj9eKrmsxiLewtdGZR9SWxddRuVd5mixG3Dq3nwSnQaPGCqq+ynxJvuYYPUE+J3YO28ocxcLT1yzxzP5EdvIoex44in1snBYElskye5A9Lc6KEBYl8J2cIpjXncHweU8=";


            ExecuteDatabaseAsync(connectString);

        }

        static void ExecuteDatabaseAsync(string connectionString)
        {
            string deviceid = "miner10001";
            string host = "192.168.0.35";
            int port = 2511;
            string user = "testMiner";
            string passwd = "1234";
            string deviceName = "부산1";

            DeviceRow row = new DeviceRow();
            row.Initialize(deviceid, host, port, user, passwd, deviceName, QueryType.Update);


            MsSQLTransaction transaction = new MsSQLTransaction();
            var dbInfo = transaction.MakeConnectInfo(connectionString);
            transaction.Connect(dbInfo.Ip, dbInfo.Port, dbInfo.DbName, dbInfo.User, dbInfo.Password);

            List<SqlTransactionQueryInfo> queries = new List<SqlTransactionQueryInfo>();

            TransactionHelper.AddSqlTransactionQueryInfo(row, ref queries);

            //transaction.Execute(queries);
        }
    }
}

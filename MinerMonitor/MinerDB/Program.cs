using MinerDB.Dac;
using MinerDB.DBConnect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MinerDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string info2 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Miner_db;User ID=ad_miner;Password=OSserver12!&;";
            //string info2 =   "Data Source=10.255.0.18;Initial Catalog=BD;User ID=ad_web;Password=webrhksflwk@!;";

            Console.WriteLine(info2);

            TripleDESCryptoService crypto = new TripleDESCryptoService();

            string enc = crypto.Encrypt(info2);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(enc);

            Console.WriteLine(crypto.Decrypt(enc));

            try
            {
                // DB 서버 연결 문제: 서버를 찾을 수 없거나 액세스 할 수 없다
                using (MinerDac dac = new MinerDac(enc))
                {
                    string spName = "Miner_db.dbo.GetServerInfo";
                    string query = "select * from Miner_db.dbo.t_server_info";
                    var temp = dac.Execute(DatabaseExecuteType.QUERY, Parameter.YES, query, new Dictionary<string, string>() { { "port", "33337" } });
                    Console.WriteLine(temp.Rows[0]["host"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

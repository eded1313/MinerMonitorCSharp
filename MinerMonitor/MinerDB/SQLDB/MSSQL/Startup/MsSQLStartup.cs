using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDB.DBConnect;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MDB.SQLDB.MSSQL.Startup
{
    static public class DBName
    {
        static public string Miner = "Miner";
    }
    static public class MsSQLStartup
    {
        //static public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        //{
        //    var sqlConfiguration = configuration.GetSection("MSSQL");

        //    if (!sqlConfiguration.Exists())
        //        throw new MissingFieldException("MSSQL");

        //    // Microsoft.Extensions.Configuration 5.0.0 version download
        //    // Nuget store [Microsoft.Extensions.Configuration.Biner ] download
        //    var connectInfo = new DbConnectInfo();
        //    (sqlConfiguration as IConfiguration).Bind(connectInfo);

        //    var factory = new MsSQLConnectorFactory(connectInfo);
        //    services.AddSingleton<IDBConnectorFactory>(factory);
        //}

        //static public void Initialize(IServiceProvider provider)
        //{
        //    QueryHandler.Initialize(provider.GetRequiredService<IDBConnectorFactory>());
        //}

        public static DbConnectInfo MSSQLConnectInfo(string connectString)
        {
            TripleDESCryptoService crypto = new TripleDESCryptoService();
            string decode = crypto.Decrypt(connectString);

            var obj = JsonConvert.DeserializeObject(decode) as JObject;

            return new DbConnectInfo()
            {
                Ip = obj.GetValue("address").ToString(),
                Port = Convert.ToInt32(obj.GetValue("port").ToString()),
                DbName = obj.GetValue("DBName").ToString(),
                User = obj.GetValue("User").ToString(),
                Password = obj.GetValue("PassWD").ToString(),
                MinPoolSize = 1,
                MaxPoolSize = 100
            };
        }
    }
}

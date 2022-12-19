using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestDatabase.SQLDB.MSSQL.Startup
{
    static public class MsSQLStartup
    {
        static public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var sqlConfiguration = configuration.GetSection("MSSQL");

            if (!sqlConfiguration.Exists())
                throw new MissingFieldException("MSSQL");

            // Microsoft.Extensions.Configuration 5.0.0 version download
            // Nuget store [Microsoft.Extensions.Configuration.Biner ] download
            var connectInfo = new DbConnectInfo();
            (sqlConfiguration as IConfiguration).Bind(connectInfo);

            var factory = new MsSQLConnectorFactory(connectInfo);
            services.AddSingleton<IDBConnectorFactory>(factory);
        }

        static public void Initialize(IServiceProvider provider)
        {
            QueryHandler.Initialize(provider.GetRequiredService<IDBConnectorFactory>());
        }
    }
}

using MinerDB.RowObject;
using System;
using System.Collections.Generic;
using System.Text;
using TestDatabase.SQLDB;
using TestDatabase.SQLDB.MSSQL;
using TestDatabase.SQLDB.MSSQL.Startup;

namespace MDatabase
{
    public class DBExecutor
    {
        private readonly DbConnectInfo _dbInfo;
        public DBExecutor(string connectString)
        {
            _dbInfo = MsSQLStartup.MSSQLConnectInfo(connectString);
        }

        public void ExecuteQuery(DeviceRow row)
        {
            MsSQLTransaction transaction = new MsSQLTransaction();
            transaction.Connect(_dbInfo.Ip, _dbInfo.Port, _dbInfo.DbName, _dbInfo.User, _dbInfo.Password);

            List<SqlTransactionQueryInfo> queries = new List<SqlTransactionQueryInfo>();

            TransactionHelper.AddSqlTransactionQueryInfo(row, ref queries);

            transaction.Execute(queries);
        }

        public void ProcedureInitialize()
        {
            MsSQLConnectorFactory factory = new MsSQLConnectorFactory(_dbInfo);
            QueryHandler.Initialize(factory);
        }
    }
}

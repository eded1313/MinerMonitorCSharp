using MDB.DBObject;
using MDB.SQLDB;
using MDB.SQLDB.MSSQL;
using MDB.SQLDB.MSSQL.Startup;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MDB
{
    public class DBExecutor
    {
        private readonly DbConnectInfo _dbInfo;
        private readonly Database _dbInstance;

        public DBExecutor(string connectString)
        {
            _dbInfo = MsSQLStartup.MSSQLConnectInfo(connectString);
            _dbInstance = new SqlDatabase(connectString);
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

        public void ExecuteDatabaseSync(TransactionObject row)
        {
            string strSql = row.CommandText;
            DbCommand dbCommand = null;

            switch (row.CommandType)
            {
                case System.Data.CommandType.Text:
                    dbCommand = _dbInstance.GetSqlStringCommand(strSql);
                    break;
                case System.Data.CommandType.StoredProcedure:
                    dbCommand = _dbInstance.GetStoredProcCommand(strSql);
                    break;
                case System.Data.CommandType.TableDirect:
                    dbCommand = _dbInstance.GetSqlStringCommand(strSql);
                    break;
                default:
                    break;
            }

            DBUtil util = new DBUtil();
            util.GetDataTable(_dbInstance.ExecuteDataSet(dbCommand));
        }
    }
}

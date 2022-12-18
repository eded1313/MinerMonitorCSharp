using MinerDB.DBConnect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace MinerDB.Dac
{
    public class MinerDac : SqlHelper
    {
        public MinerDac(string connectionString)
            : base(connectionString)
        {
        }

        public override DataTable Execute(DatabaseExecuteType executeType, Parameter isParameter, string command, Dictionary<string, string> parameters)
        {
            return executeType switch
            {
                DatabaseExecuteType.STORE_PROCEDURE => ExecuteStoreProcedure(isParameter, command, parameters),
                DatabaseExecuteType.QUERY => ExecuteQuery(isParameter, command, parameters),
                _ => throw new NotImplementedException(),
            };
        }

        private DataTable ExecuteStoreProcedure(Parameter isParameter, string spName, Dictionary<string, string> parameters)
        {
            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

            if (isParameter == Parameter.YES && parameters != null)
            {
                MakeProcedureParameter(dbCommand, parameters);
            }

            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
        }

        private DataTable ExecuteQuery(Parameter isParameter, string query, Dictionary<string, string> parameters)
        {
            //string query = "select * from Miner_db.dbo.t_server_info";
            
            if (isParameter == Parameter.YES && parameters != null)
            {
                query += MakeQueryParameter(parameters);
            }

            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(query);

            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDB.SQLDB
{
    public enum QueryType
    {
        None,
        Select,
        Insert,
        InsertOutput,
        Update,
        Delete
    }
    public enum DBResult
    {
        /// <summary>
        /// 실행되지 않음
        /// </summary>
        NONE = 0,
        /// <summary>
        /// 성공 -> Commit
        /// </summary>
        SUCCESS = 1,
        /// <summary>
        /// 처리 실패 -> Rollback
        /// </summary>
        FAIL = 2
    }
    
    public interface ISqlTransactionQuery
    {
        CommandType CommandType { get; }
        string CommandText { get; }
        bool IsDirty { get; }
        void BindParameter(IDbTransactionConnector connector);
        bool IsScalar { get; }
        Action<object> ExecuteScalarAfterEvent { get; }
        bool SetOutputParameter(DbParameterCollection parameters);
        void UpdateExectueType(QueryType type);
        public int ReturnCode { get; }
        public DBResult Result { get; }
        public string ResultMsg { get; }
        void UpdateExcuteResult(DBResult result, string message);
        string GetParameterToString();
        void ClearParameter();
    }

    public class SqlTransactionQueryInfo
    {
        public CommandType CommandType { get; set; }
        public string CommandText { get; set; }
        public List<ISqlTransactionQuery> Queries { get; set; }
    }

    public static class TransactionHelper
    { 
        public static void AddSqlTransactionQueryInfo(ISqlTransactionQuery row, ref List<SqlTransactionQueryInfo> queries)
        {
            var queryCommand = row.CommandText;
            var info = queries.Where(q => q.CommandText.Equals(queryCommand)).FirstOrDefault();
            if (info is null)
            {
                queries.Add(
                    new SqlTransactionQueryInfo
                    {
                        CommandType = row.CommandType,
                        CommandText = queryCommand,
                        Queries = new List<ISqlTransactionQuery> { row }
                    });
            }
            else
            {
                info.Queries.Add(row);
            }

        }
    }


    public interface IDbTransactionConnector : IDisposable
    {
        string ErrorMessage { get; }
        void Connect(string ip, int port, string dbName, string user, string password, int minPoolSize, int maxPoolSize);
        void BindParameter(string name, object value);
        void BindOutputParameter(string name, SqlDbType dbType);
        //bool Execute(List<ISqlTransactionQuery> queries);
        //bool Execute(params ISqlTransactionQuery[] queries);
        //bool Execute(Action<DbCommand> action);
        //Task<bool> ExecuteAsync(Dictionary<string, List<ISqlTransactionQuery>> queries);
        bool Execute(List<SqlTransactionQueryInfo> queryInfos);
    }
}

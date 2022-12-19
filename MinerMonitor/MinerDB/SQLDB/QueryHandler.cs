using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDatabase.SQLDB
{
    public interface IQueryHandler : IDisposable
    {
        string ErrorMessage { get; }
        Task<bool> ExecuteAsync();
    }
    /// <summary>
    /// IDBLogger interface 객체 NULL 허용 (Nullable),
    /// Cheat의 경우 로그 객체 null 값
    /// </summary>
    public interface IDBLogger : IDisposable
    {
        void WriteLog(DBResult result, string CommandText, string Parameter, string Message);
    }
    public abstract class QueryHandler : IQueryHandler
    {
        static public IDBConnectorFactory DBConnectorFactory { get; private set; }

        static public void Initialize(IDBConnectorFactory dBConnectorFactory)
        {
            DBConnectorFactory = dBConnectorFactory;
        }

        public abstract string CommandString { get; }
        public abstract CommandType CommnadType { get; }
        /// <summary>
        /// IDBLogger interface 객체 NULL 허용 (Nullable)
        /// </summary>
        public IDBLogger DbLogger { get; set; }


        //protected IDbConnector _dBConnector;

        protected int _dbID;
        protected string _dbName;
        //protected DbConnectInfo _dbConnectInfo;

        public string ErrorMessage { get; private set; }
        //public string ParameterString { get; private set; }

        //public QueryHandler()
        //{
        //}

        public QueryHandler(string dbName, int dbID, IDBLogger dbLogger)
        {
            _dbID = dbID;
            _dbName = dbName;
            DbLogger = dbLogger;

        }

        public bool Execute()
        {
            try
            {
                using var dBConnector = DBConnectorFactory.Create(_dbID, _dbName);

                dBConnector.SetCommand(CommnadType, CommandString);
                bindParamters(dBConnector);
                
                if (!dBConnector.Execute(loader))
                {
                    ErrorMessage = $"[StoredProcedure:{CommandString}] {dBConnector.ErrorMessage}";
                    DbLogger?.WriteLog(DBResult.FAIL, CommandString, dBConnector.GetParameterToString(), dBConnector.ErrorMessage);
                    return false;
                }

                setOutputValue(dBConnector.Outputs);
                DbLogger?.WriteLog(DBResult.SUCCESS, CommandString, dBConnector.GetParameterToString(), string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"[StoredProcedure:{CommandString}] {ex.Message}";
                return false;
            }
        }
        
        public virtual async Task<bool> ExecuteAsync()
        {
            try
            {
                using var dBConnector = await DBConnectorFactory.CreateAsync(_dbID, _dbName);

                dBConnector.SetCommand(CommnadType, CommandString);
                bindParamters(dBConnector);

                var result = dBConnector.Execute(loader);
                if (!result)
                {
                    ErrorMessage = $"[StoredProcedure:{CommandString}] {dBConnector.ErrorMessage}";
                    DbLogger?.WriteLog(DBResult.FAIL, CommandString, dBConnector.GetParameterToString(), dBConnector.ErrorMessage);

                    return false;
                }

                setOutputValue(dBConnector.Outputs);
                DbLogger?.WriteLog(DBResult.SUCCESS, CommandString, dBConnector.GetParameterToString(), string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"[StoredProcedure:{CommandString}] {ex.Message}";
                return false;
            }

        }

        protected virtual void bindParamters(IDbConnector connector) { }
        protected virtual void loader(DbDataReader dataReader, int resultId) { }
        protected virtual void setOutputValue(IReadOnlyDictionary<string, object> outputValues) { }

        public void Dispose()
        {
            //_dBConnector?.Dispose();
            //GC.SuppressFinalize(this);
        }
    }
}

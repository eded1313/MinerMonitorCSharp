using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDatabase.SQLDB
{   
    public class TableQueryHandler : IDisposable
    {
        static public IDBConnectorFactory DBConnectorFactory => QueryHandler.DBConnectorFactory;

        protected IDbConnector _dBConnector;

        protected string _dbName;

        protected int _dbIndex;


        public string ErrorMessage { get; protected set; }


        public TableQueryHandler(string dbName, int dbIndex = 0)
        {
            _dbName = dbName;
            _dbIndex = dbIndex;
        }

        public bool Execute(TableQueryObj inQuery)
        {
            try
            {
                _dBConnector = DBConnectorFactory.Create(_dbIndex, _dbName);
               
                _dBConnector.SetCommand(inQuery.CommandType, inQuery.CommandString);
                inQuery.BindParamtersFunc();

                if (!_dBConnector.Execute(inQuery.LoaderFunc))
                {
                    ErrorMessage = $"[{inQuery.CommandType.ToString()}:{inQuery.CommandString}] {_dBConnector.ErrorMessage}";
                    return false;
                }

                inQuery.SetOutputValueFunc(_dBConnector.Outputs);
                
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"[{inQuery.CommandType.ToString() ?? ""}:{inQuery.CommandString ?? ""}] {ex.Message}";
                return false;
            }
        }


        public void Dispose()
        {
            _dBConnector?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

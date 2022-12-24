using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDB.SQLDB
{
    public class DbConnectInfo
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public string DbName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int MinPoolSize { get; set; }
        public int MaxPoolSize { get; set; }

        public override bool Equals(object obj)
        {
            //if (obj is not DbConnectInfo cmpObj)
            //    return false;
            DbConnectInfo cmpObj;

            if (obj.GetType() != typeof(DbConnectInfo))
                return false;
            else
                cmpObj = obj as DbConnectInfo;

            if (this.Ip.Equals(cmpObj.Ip))
                return false;

            if (this.Port != cmpObj.Port)
                return false;

            if (this.DbName.Equals(cmpObj.DbName))
                return false;

            if (this.User.Equals(cmpObj.User))
                return false;

            if (this.Password.Equals(cmpObj.Password))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }

    public interface IDbConnector : IDisposable
    {
        bool HasRow {get;}
        int AffectedRowCount { get; }
        IReadOnlyDictionary<string, object> Outputs { get; }
        string ErrorMessage { get; }

        void Connect(string ip, int port, string dbName, string user, string password, int minPoolSize, int maxPoolSize);
        void SetCommand(CommandType commandType, string queryString);
        void BindParameter(string name, object value, ParameterDirection direction);
        void BindOutputParameter(string name, SqlDbType dbType);
        bool Execute(Action<DbDataReader, int> rowReadAction);
        ValueTask<bool> ExecuteAsync(Action<DbDataReader, int> rowReadAction);
        string GetParameterToString();
    }
}

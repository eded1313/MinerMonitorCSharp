using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDatabase.SQLDB
{
    public interface IDBConnectorFactory
    {
        //static IDBConnectorFactory Instance { get; set; }
        void SetGlobalDBInfo(List<DbConnectInfo> _globalConnectionInfo);
        IDbConnector Create(int worldID, string dbName);
        Task<IDbConnector> CreateAsync(int worldID, string dbName);
        IDbTransactionConnector CreateTransaction(int dbID, string dbName);
        Task<IDbTransactionConnector> CreateTransactionAsync(int dbID, string dbName);
        void AddConnectInfo(int dbID, DbConnectInfo info);
        void RemoveConnectInfo(int dbID, string dbName);
    }
}

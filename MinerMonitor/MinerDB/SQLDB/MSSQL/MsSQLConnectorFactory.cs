using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MDB.SQLDB.MSSQL
{
    public class SingletoneServiceObject<T>
    {
#if DEBUG
        static private int allocCount = 0;
#endif

        public SingletoneServiceObject()
        {
#if DEBUG
            var createCount = Interlocked.Increment(ref SingletoneServiceObject<T>.allocCount);
            if (createCount > 1)
                throw new Exception($"call duplicated object. {typeof(T).FullName}");
#endif
        }
    }

    public class MsSQLConnectorFactory : SingletoneServiceObject<MsSQLConnectorFactory>, IDBConnectorFactory
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private Dictionary<int, Dictionary<string, DbConnectInfo>> _dbConnectionInfos = new Dictionary<int, Dictionary<string, DbConnectInfo>>();

        public static int ConnectionCount;



        public MsSQLConnectorFactory(DbConnectInfo connectionInfo)
        {
            _dbConnectionInfos.Add(0, new Dictionary<string, DbConnectInfo> { { connectionInfo.DbName, connectionInfo } });

            ConnectionCount = 0;
        }

        public void SetGlobalDBInfo(List<DbConnectInfo> _globalConnectionInfo)
        {
            _globalConnectionInfo.ForEach(conn => _dbConnectionInfos[0].Add(conn.DbName, conn));
        }
        
        public IDbConnector Create(int dbID, string dbName)
        {
            DbConnectInfo connectInfo = getConnectionInfo(dbID, dbName);
            if (connectInfo == null)
                return null;

            var connector = new MsSQLConnector();
            connector.Connect(connectInfo.Ip, connectInfo.Port, connectInfo.DbName, connectInfo.User, connectInfo.Password, connectInfo.MinPoolSize, connectInfo.MaxPoolSize);

            return connector;
        }

        public async Task<IDbConnector> CreateAsync(int dbID, string dbName)
        {
            DbConnectInfo connectInfo = getConnectionInfo(dbID, dbName);
            if (connectInfo == null)
                return null;

            var connector = new MsSQLConnector();
            await connector.ConnectAsync(connectInfo.Ip, connectInfo.Port, connectInfo.DbName, connectInfo.User, connectInfo.Password, connectInfo.MinPoolSize, connectInfo.MaxPoolSize);

            return connector;
        }


        public IDbTransactionConnector CreateTransaction(int dbID, string dbName)
        {
            DbConnectInfo connectInfo = getConnectionInfo(dbID, dbName);
            if (connectInfo == null)
                return null;


            var connector = new MsSQLTransaction();
            connector.Connect(connectInfo.Ip, connectInfo.Port, connectInfo.DbName, connectInfo.User, connectInfo.Password, connectInfo.MinPoolSize, connectInfo.MaxPoolSize);

            return connector;
        }

        public async Task<IDbTransactionConnector> CreateTransactionAsync(int dbID, string dbName)
        {
            DbConnectInfo connectInfo = getConnectionInfo(dbID, dbName);
            if (connectInfo == null)
                return null;


            var connector = new MsSQLTransaction();
            await connector.ConnectAsync(connectInfo.Ip, connectInfo.Port, connectInfo.DbName, connectInfo.User, connectInfo.Password, connectInfo.MinPoolSize, connectInfo.MaxPoolSize);

            return connector;
        }

        private DbConnectInfo getConnectionInfo(int dbID, string dbName)
        {
            _lock.EnterReadLock();
            try
            {
                if (!_dbConnectionInfos.TryGetValue(dbID, out var infos))
                    return null;

                if (!infos.TryGetValue(dbName, out var connectInfo))
                    return null;

                return connectInfo;
            }
            finally
            {
                _lock.ExitReadLock();
            }

        }


        public void AddConnectInfo(int dbID, DbConnectInfo info)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                if (_dbConnectionInfos.TryGetValue(dbID, out var dbInfos))
                {
                    if (dbInfos.ContainsKey(info.DbName))
                    {
                        if (dbInfos[info.DbName].Equals(info))
                            return;
                    }
                }

                _lock.EnterWriteLock();
                try
                {
                    if (!_dbConnectionInfos.TryGetValue(dbID, out var infos))
                    {
                        infos = new Dictionary<string, DbConnectInfo>();
                        _dbConnectionInfos.Add(dbID, infos);
                    }

                    if (!infos.ContainsKey(info.DbName))
                    {
                        infos.Add(info.DbName, info);
                    }
                    else
                    {
                        infos[info.DbName] = info;
                    }
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }

        }

        public void RemoveConnectInfo(int dbID, string dbName)
        {
            _lock.EnterWriteLock();
            try
            {
                if (!_dbConnectionInfos.TryGetValue(dbID, out var infos))
                    return;

                infos.Remove(dbName);

            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}

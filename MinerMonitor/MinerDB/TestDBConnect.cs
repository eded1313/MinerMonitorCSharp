using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TestDatabase.SQLDB;

namespace MinerDB
{
    public class TestDBConnect : QueryHandler
    {
        public TestDBConnect(string dbName, int dbID, IDBLogger dbLogger) : base(dbName, dbID, dbLogger)
        {
        }

        public override string CommandString => throw new NotImplementedException();

        public override CommandType CommnadType => throw new NotImplementedException();
    }
}

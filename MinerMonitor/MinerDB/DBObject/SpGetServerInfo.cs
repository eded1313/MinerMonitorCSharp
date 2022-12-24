using MDB.SQLDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace MDB.DBObject
{
    public class SpetServerInfo : QueryHandler
    {
        public SpetServerInfo(string dbName, int dbID, IDBLogger dbLogger) : base(dbName, dbID, dbLogger)
        {
            DeviceList = new List<DeviceRow>();
        }

        public List<DeviceRow> DeviceList { get; private set; }

        public override string CommandString => "GetServerInfo";

        public override CommandType CommnadType => CommandType.StoredProcedure;

        protected override void bindParamters(IDbConnector connector)
        {
            base.bindParamters(connector);
        }

        protected override void loader(DbDataReader dataReader, int resultId)
        {
            DeviceList.Add(new DeviceRow(
                dataReader["device_id"].ToString(),
                dataReader["host"].ToString(),
                Convert.ToInt32(dataReader["port"].ToString()),
                dataReader["user_id"].ToString(),
                dataReader["password"].ToString(),
                dataReader["device_name"].ToString(),
                false
            ));
        }
    }
}

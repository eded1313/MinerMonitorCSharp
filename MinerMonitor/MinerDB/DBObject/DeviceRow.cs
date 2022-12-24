using MDB.SQLDB;
using System;
using System.Collections.Generic;
using System.Text;


namespace MDB.DBObject
{
    public class DeviceRow : TransactionQuery
    {
        protected override string TableName => "t_device_info";

        public string Device
        {
            get { return (string)GetValue("device_id"); }
            set { SetValue("device_id", value); }
        }

        public string Host
        {
            get { return (string)GetValue("host"); }
            set { SetValue("host", value); }
        }

        public int Port
        {
            get { return (int)GetValue("port"); }
            set { SetValue("port", value); }
        }

        public string User
        {
            get { return (string)GetValue("user"); }
            set { SetValue("user", value); }
        }

        public string PassWord
        {
            get { return (string)GetValue("passwd"); }
            set { SetValue("passwd", value); }
        }

        public string DeviceName
        {
            get { return (string)GetValue("deviceName"); }
            set { SetValue("deviceName", value); }
        }

        public DeviceRow(string device_id, string host, int port, string user, string passwd, string deviceName, bool isNew)
        {
            Initialize(device_id, host, port, user, passwd, deviceName, isNew);
        }

        private void Initialize(string device_id, string host, int port, string user, string passwd, string deviceName, bool isNew)
        {
            _rowValues = new Dictionary<string, RowValue>()
            {
                { "device_id", new RowValue("device_id", device_id, keyIndex: 1) },
                { "host", new RowValue("host", host) },
                { "port", new RowValue("port", port) },
                { "user", new RowValue("user_id", user) },
                { "passwd", new RowValue("password", passwd) },
                { "deviceName", new RowValue("device_name", deviceName) }
            };

            if(isNew)
                UpdateExectueType(QueryType.Insert);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TestDatabase.SQLDB;

namespace MinerDB.RowObject
{
    public class DeviceRow : TransactionQuery
    {
        protected override string TableName => "t_device_info";

        private string _host;
        public string Host
        {
            get { return _host; }
            set { _host = value; SetValue("host", value); }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; SetValue("host", value); }
        }

        private string _user;
        public string User
        {
            get { return _user; }
            set { _user = value; SetValue("host", value); }
        }

        private string _passwd;
        public string PassWord
        {
            get { return _passwd; }
            set { _passwd = value; SetValue("host", value); }
        }

        private string _deviceName;
        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; SetValue("host", value); }
        }

        public DeviceRow()
        {
        }

        public void Initialize(string device_id, string host, int port, string user, string passwd, string deviceName, QueryType executeType)
        {
            _rowValues = new Dictionary<string, RowValue>()
            {
                { "device", new RowValue("device_id", device_id, keyIndex: 0) },
                { "host", new RowValue("host", host) },
                { "port", new RowValue("port", port) },
                { "user", new RowValue("user_id", user) },
                { "passwd", new RowValue("password", passwd) },
                { "deviceName", new RowValue("device_name", deviceName) }
            };
            
            UpdateExectueType(executeType);
        }
    }
}

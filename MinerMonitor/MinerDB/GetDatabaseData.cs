using System;
using System.Collections.Generic;
using System.Text;
using TestDatabase.SQLDB;

namespace TestDatabase
{
    class GetDatabaseDataUsingProcedure : TransactionStoredProcedure
    {
        protected override string InsertCommand => base.InsertCommand;
        protected override string UpdateCommand => base.UpdateCommand;
        protected override string InsertOutputCommand => base.InsertOutputCommand;
        protected override string DeleteCommand => base.DeleteCommand;

        private string _host;
        public string Host
        {
            get { return _host; }
            set { _host = value; UpdateExectueType(QueryType.Update); }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; UpdateExectueType(QueryType.Update); }
        }

        private string _user;
        public string User
        {
            get { return _user; }
            set { _user = value; UpdateExectueType(QueryType.Update); }
        }

        private string _passwd;
        public string PassWord
        {
            get { return _passwd; }
            set { _passwd = value; UpdateExectueType(QueryType.Update); }
        }

        private string _deviceName;
        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; UpdateExectueType(QueryType.Update); }
        }

        public GetDatabaseDataUsingProcedure(string host, int port, string user, string passwd, string deviceName, bool isNew)
        {
            Initialize(host, port, user, passwd, deviceName, isNew);
        }

        private void Initialize(string host, int port, string user, string passwd, string deviceName, bool isNew)
        {
            _host = host;
            _port = port;
            _user = user;
            _passwd = passwd;
            _deviceName = deviceName;

            if (isNew)
                UpdateExectueType(QueryType.Insert);
        }

        protected override void collectStoredProcedureParamters()
        {
            switch (ExecuteType)
            {
                case QueryType.Insert:
                    _parameter.Add("@in_host", _host);
                    _parameter.Add("@in_port", _port);
                    _parameter.Add("@in_user", _user);
                    _parameter.Add("@in_passwd", _passwd);
                    _parameter.Add("@in_device", _deviceName);
                    break;
                case QueryType.Update:
                    _parameter.Add("@in_host", _host);
                    _parameter.Add("@in_port", _port);
                    _parameter.Add("@in_user", _user);
                    _parameter.Add("@in_passwd", _passwd);
                    _parameter.Add("@in_device", _deviceName);
                    break;
            }

        }
    }

    class GetDatabaseUsingQuery : TransactionQuery
    {
        protected override string TableName => "t_miner_server";

        private string _host;
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private string _user;
        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _passwd;
        public string PassWord
        {
            get { return _passwd; }
            set { _passwd = value; }
        }

        private string _deviceName;
        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        public GetDatabaseUsingQuery(string host, int port, string user, string passwd, string deviceName, bool isNew)
        {
            Initialize(host, port, user, passwd, deviceName, isNew);
        }

        private void Initialize(string host, int port, string user, string passwd, string deviceName, bool isNew)
        {
            _rowValues = new Dictionary<string, RowValue>()
            {
                { "host", new RowValue("db_host", host, keyIndex: 1) },
                { "port", new RowValue("db_port", port, keyIndex: 2) },
                { "user", new RowValue("db_user", user) },
                { "passwd", new RowValue("db_passwd", passwd) },
                { "deviceName", new RowValue("db_device", deviceName) }
            };

            if (isNew)
                UpdateExectueType(QueryType.Insert);
        }
    }
}

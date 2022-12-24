using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDB.SQLDB
{
    public class TableQueryLoader : TableQueryHandler
    {
        private readonly string _infoTableName;
        private readonly string _typeMD5ColumnName;
        private readonly string _dataMD5ColumnName;

        public TableQueryLoader(string dbName, string infoTableName, string inTypeMD5ColumnName, string inDataMD5ColumnName)
           : base(dbName)
        {
            _infoTableName = infoTableName;
            _typeMD5ColumnName = inTypeMD5ColumnName;
            _dataMD5ColumnName = inDataMD5ColumnName;
        }

        public bool TableDataLoad(string inTableName, string inTypeMD5, string inCommandString, Action<string> inAddTableContext)
        {
            string dataMD5 = string.Empty, typeMD5 = string.Empty;

            // TableInfo 
            Execute(TableQueryObj.Create(CommandType.Text, $"select top 1 {_typeMD5ColumnName}, {_dataMD5ColumnName} from {_infoTableName} where c_name = '{inTableName}'",
                (dataReader, resultId) =>
                {
                    dataMD5 = dataReader["_dataMD5ColumnName"].ToString();
                    typeMD5 = dataReader["_typeMD5ColumnName"].ToString();
                }));

            if (inTypeMD5.CompareTo(typeMD5) == 0)
            {
                return false;
            }


            return Execute(TableQueryObj.Create(CommandType.Text, inCommandString,
                (dataReader, resultId) =>
                {
                    object[] values = new object[dataReader.FieldCount];
                    dataReader.GetValues(values);
                    inAddTableContext(string.Join(",", values));
                }));
        }
    }

}
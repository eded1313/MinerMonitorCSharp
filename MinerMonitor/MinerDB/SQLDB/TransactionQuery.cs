using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MDB.SQLDB
{
    public abstract class TransactionQuery : TransactionObject
    {
        [JsonIgnore]
        public override CommandType CommandType => CommandType.Text;
        protected abstract string TableName { get; }

        protected Dictionary<string, RowValue> _rowValues;
        protected override string InsertCommand
        {
            get
            {
                var columns = _rowValues.Where(kv => !kv.Value.IsDefaultSetAtDB).Select(kv => kv.Value.ColumnName);
                return $"INSERT INTO {TableName} ({string.Join(',', columns)}) VALUES({string.Join(',', columns.Select(s => "@" + s))});";
            }
        }

        protected override string InsertOutputCommand
        {
            get
            {
                var columns = _rowValues.Where(kv => !kv.Value.IsDefaultSetAtDB).Select(kv => kv.Value.ColumnName);
                var keyColumn = _rowValues.Where(kv => kv.Value.KeyIndex > 0).OrderBy(kv => kv.Value.KeyIndex).Select(kv => kv.Value.ColumnName).SingleOrDefault();
                return $"INSERT INTO {TableName} ({string.Join(',', columns)}) OUTPUT inserted.{keyColumn} VALUES({string.Join(',', columns.Select(s => "@" + s))});";
            }
        }


        protected override string UpdateCommand
        {
            get
            {
                var columns = _rowValues.Where(kv => kv.Value.KeyIndex == 0 && kv.Value.IsDirty).Select(kv => $"{kv.Value.ColumnName}=@{kv.Value.ColumnName}");
                var keyColumns = _rowValues.Where(kv => kv.Value.KeyIndex > 0).OrderBy(kv => kv.Value.KeyIndex).Select(kv => $"{kv.Value.ColumnName}=@{kv.Value.ColumnName}");

                return $"UPDATE {TableName} SET {string.Join(',', columns)} WHERE {string.Join(" and ", keyColumns)};";
            }
        }

        protected override string DeleteCommand
        {
            get
            {
                var keyColumns = _rowValues.Where(kv => kv.Value.KeyIndex > 0).OrderBy(kv => kv.Value.KeyIndex).Select(kv => $"{kv.Value.ColumnName}=@{kv.Value.ColumnName}");
                return $"DELETE FROM {TableName} WHERE {string.Join(" and ", keyColumns)};";
            }
        }

        protected object GetValue(string key)
        {
            if (!_rowValues.TryGetValue(key, out var rowValue))
                return null;

            return rowValue.Value;
        }

        protected void SetValue(string key, object value)
        {
            _rowValues[key].Value = value;
            UpdateExectueType(QueryType.Update);
        }

        protected virtual void bindParameterForInsert(IDbTransactionConnector connector)
        {
            bindRowvalueParameter(connector, kv => !kv.IsDefaultSetAtDB);
        }

        protected virtual void bindParameterForUpdate(IDbTransactionConnector connector)
        {
            bindRowvalueParameter(connector, kv => kv.KeyIndex > 0 || kv.IsDirty);
        }

        protected virtual void bindParameterForDelete(IDbTransactionConnector connector)
        {
            bindRowvalueParameter(connector, kv => kv.KeyIndex > 0);
        }

        private void bindRowvalueParameter(IDbTransactionConnector connector, Func<RowValue, bool> predicate)
        {
            foreach (var val in _rowValues.Values.Where(predicate))
            {
                connector.BindParameter("@" + val.ColumnName, val.Value);
                _parameter.Add(val.ColumnName, val.Value.ToString());
            }
        }

        //private void bindOutParameter(IDbTransactionConnector connector, Func<RowValue, bool> predicate)
        //{
        //    foreach (var val in _rowValues.Values.Where(predicate))
        //    {
        //        connector.BindParameter("@" + val.ColumnName, val.Value);
        //    }
        //}

        public override void BindParameter(IDbTransactionConnector connector)
        {
            switch (ExecuteType)
            {
                case QueryType.Insert:
                case QueryType.InsertOutput:
                    bindParameterForInsert(connector);
                    break;
                case QueryType.Update:
                    bindParameterForUpdate(connector);
                    break;
                case QueryType.Delete:
                    bindParameterForDelete(connector);
                    break;
            }
        }

        public override bool SetOutputParameter(DbParameterCollection parameters)
        {
            return true;
        }
    }
}

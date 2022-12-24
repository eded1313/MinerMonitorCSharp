using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDB.SQLDB
{
    public class RowValue
    {
        public bool IsDirty { get; private set; }
        public int KeyIndex { get; private set; }
        public bool IsDefaultSetAtDB { get; private set; }
        public string ColumnName { get; set; }
        public object Value
        {
            get { return _value; }
            set
            {
                IsDirty = true;
                _value = value;
            }
        }

        private object _value;

        public RowValue(string column, int keyIndex, bool isDefaultSet)
        {
            ColumnName = column;
            KeyIndex = keyIndex;
            IsDefaultSetAtDB = isDefaultSet;
        }

        public RowValue(string column, object value, int keyIndex = 0, bool isDefaultSet = false)
        {
            ColumnName = column;
            KeyIndex = keyIndex;
            IsDefaultSetAtDB = isDefaultSet;
            _value = value;
        }

        //public static implicit operator long(RowValue r) => (long)r.Value;
        //public static implicit operator string(RowValue r) => (string)r.Value;
    }
}

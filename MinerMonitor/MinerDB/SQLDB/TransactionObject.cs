using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestDatabase.SQLDB
{
    public abstract class TransactionObject : ISqlTransactionQuery
    {
        [JsonIgnore]
        public virtual CommandType CommandType { get; } = CommandType.Text;
        
        protected abstract string InsertCommand { get; }
        protected abstract string InsertOutputCommand { get; }
        protected abstract string UpdateCommand { get; }
        protected abstract string DeleteCommand { get; }

        [JsonIgnore]
        public QueryType ExecuteType { get; set; }
        [JsonIgnore]
        public bool IsDirty => ExecuteType != QueryType.None && ExecuteType != QueryType.Select;
        [JsonIgnore]
        public bool IsScalar => ExecuteType == QueryType.InsertOutput;
        [JsonIgnore]
        public Action<object> ExecuteScalarAfterEvent { get; protected set; }
        [JsonIgnore]
        public string CommandText
        {
            get
            {
                return ExecuteType switch
                {
                    QueryType.Insert => InsertCommand,
                    QueryType.InsertOutput => InsertOutputCommand,
                    QueryType.Update => UpdateCommand,
                    QueryType.Delete => DeleteCommand,
                    _ => string.Empty
                };
            }
        }

        [JsonIgnore]
        public int ReturnCode { get; set; } = 0;

        protected Dictionary<string, object> _parameter = new Dictionary<string, object>();

        public DBResult Result { get; private set; }
        public string ResultMsg { get; private set; } = string.Empty;


        public abstract void BindParameter(IDbTransactionConnector connector);
        public abstract bool SetOutputParameter(DbParameterCollection parameters);

        public virtual void UpdateExectueType(QueryType type)
        {
            if( ExecuteType == QueryType.Insert || ExecuteType == QueryType.InsertOutput)
            {
                if (type == QueryType.Update)
                    return;
            }

            ExecuteType = type;
        }

        public void UpdateExcuteResult(DBResult result, string msg)
        {
            Result = result;
            ResultMsg = msg;
        }

        public virtual string GetParameterToString()
        {
            return string.Join(",", _parameter.Where(x => x.Value != null).Select(kvp =>
                    kvp.Value.GetType().Equals(typeof(DateTime)) ? 
                        $"[{kvp.Key}, {((DateTime)kvp.Value).ToString("yyyy-MM-ddTHH:mm:sszzz")}]" : kvp.ToString()
                )
            );
        }


        public virtual void ClearParameter()
        {
            _parameter.Clear();
        }

    }
}

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
    public abstract class TransactionStoredProcedure : TransactionObject
    {
        [JsonIgnore]
        public override CommandType CommandType => CommandType.StoredProcedure;
        protected override string InsertOutputCommand => throw new NotImplementedException("TransactionStoredProcedure is not implemented InsertOutputCommand");
        protected override string InsertCommand => throw new NotImplementedException("TransactionStoredProcedure is not implemented InsertCommand");
        protected override string UpdateCommand => throw new NotImplementedException("TransactionStoredProcedure is not implemented UpdateCommand");
        protected override string DeleteCommand => throw new NotImplementedException("TransactionStoredProcedure is not implemented DeleteCommand");

        public override void BindParameter(IDbTransactionConnector connector)
        {
            _parameter.Clear();
            collectStoredProcedureParamters();

            foreach (var kv in _parameter)
            {
                connector.BindParameter(kv.Key, kv.Value);
            }
            connector.BindOutputParameter("@out_return_code", SqlDbType.Int);
        }

        /// <summary>
        /// bind할 parameter와 값을 _parameter 객체에 수집하는 함수로 stored procedure에서 사용한다.
        /// </summary>
        protected abstract void collectStoredProcedureParamters();

        public override bool SetOutputParameter(DbParameterCollection parameters)
        {
            ReturnCode = Convert.ToInt32(parameters["@out_return_code"].Value);
            if (ReturnCode != 0)
            {
                return false;
            }

            return setOutputParameterValue(parameters);
        }

        /// <summary>
        /// 각 row에서 추가적인 output값을 가져올때 사용
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected virtual bool setOutputParameterValue(DbParameterCollection parameters)
        {
            return true;
        }
    }
}

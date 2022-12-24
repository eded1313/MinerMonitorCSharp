using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDB.DBConnect
{
    public abstract class SqlHelper : IDisposable
    {
        #region ================= 공통 =================
        /// <summary>
        /// 필요한 경우 오류 메시지를 기록
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// DataTable의 Rows Count
        /// </summary>
        protected int dataCount = 0;

        /// <summary>
        /// DataTable의 Rows Count
        /// </summary>
        public int DataCount
        {
            get { return dataCount; }
            protected set { dataCount = value; }
        }
        #endregion

        #region ================= 생성자 및 초기화 =================
        protected SqlHelper(string connType)
        {
            string strConnection = GetConnectionString(connType);
            //DbAccess = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(strConnection);
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

        #region ================= Properties =================
        //protected Database DbAccess;
        #endregion

        #region ================= Method =================
        public abstract DataTable Execute(DatabaseExecuteType executeType, Parameter isParameter, string command, Dictionary<string, string> parameters);

        private string GetConnectionString(string connType)
        {
            TripleDESCryptoService crypto = new TripleDESCryptoService();

            return crypto.Decrypt(connType);
        }

        /// <summary>
        /// DataSet에서 첫번째 테이블을 DataTable 형식으로 반환
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>DataTable</returns>
        protected DataTable GetDataTable(DataSet ds)
        {
            return GetDataTable(ds, 0);
        }

        /// <summary>
        /// DataSet에서 index의 DataTable을 반환한다.
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="index">반환할 DataTable Index</param>
        /// <returns>DataTable</returns>
        protected DataTable GetDataTable(DataSet ds, int index)
        {
            DataTable dt = null;

            if (ds != null && ds.Tables.Count > index)
            {
                dt = ds.Tables[index].Copy();
            }

            return dt;
        }

        protected void MakeProcedureParameter(DbCommand dbCommand, Dictionary<string, string> parameters)
        {
            foreach (var (key, value) in parameters)
            {
                //this.DbAccess.AddInParameter(dbCommand, key, DbType.AnsiString, value);
            }
        }

        protected string MakeQueryParameter(Dictionary<string, string> parameters)
        {
            string parameter = " where ";

            int index = 0;
            foreach (var (key, value) in parameters)
            {
                if(index == 0)
                    parameter += (key + "=" + "'"+value+"'");
                else
                    parameter += ("and " + key + "=" + "'" + value + "'");

            }

            return parameter;
        }
        #endregion
    }
}

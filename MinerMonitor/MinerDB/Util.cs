using System;
using System.Collections.Generic;
using System.Data;

namespace MDB
{
    public static class RandomDevice
    {
        public static int GetRandomID(List<int> alreadyIDs = null)
        {
            Random random = new Random();
            int result = 0;

            if (alreadyIDs != null)
            {
                do
                {
                    result = random.Next(10000, 99999);

                    if (alreadyIDs.Contains(result))
                        result = 0;
                }
                while (result == 0);

                return result;
            }
            else
                return random.Next(10000, 99999);
        }
    }

    public class DBConnType
    {
        public DBConnType()
        {

        }

        public static string MinerServer
        {
            get
            {
                return "d7Pt1WHH9OZCj9eKrmsxiLewtdGZR9SWxddRuVd5mixG3Dq3nwSnQaPGCqq+ynxJvuYYPUE+J3YO28ocxcLT1yzxzP5EdvIoex44in1snBYElskye5A9Lc6KEBYl8J2cIpjXncHweU8=";
            }
        }
    }

    public class DBUtil
    {
        /// <summary>
        /// DataSet에서 첫번째 테이블을 DataTable 형식으로 반환
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(DataSet ds)
        {
            return GetDataTable(ds, 0);
        }

        /// <summary>
        /// DataSet에서 index의 DataTable을 반환한다.
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="index">반환할 DataTable Index</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(DataSet ds, int index)
        {
            DataTable dt = null;

            if (ds != null && ds.Tables.Count > index)
            {
                dt = ds.Tables[index].Copy();
            }

            return dt;
        }
    }
}

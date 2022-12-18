using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinerDB.DBConnect
{
    public enum DatabaseExecuteType
    { 
        NONE,
        STORE_PROCEDURE,
        QUERY,
    }

    public enum Parameter
    { 
        NO,
        YES,
    }

    class DBConnType
    {
        DBConnType()
        {

        }

        static string Service_Miner
        {
            get
            {
                return "y/sBAzgP8JMDx2hEaZFX4vZl1jdeiOa4Z7fxSdTeQslQmtW0eImdzQLmjZOsHV0YGiIaQT4v9PxLvmg0AjhW8DAE3u+K7NmZNBL3/m7rVWIi8kSZM8A+bijfZhE1Q4SQdgv0xxoG5MY=";
            }
        }
    }
}

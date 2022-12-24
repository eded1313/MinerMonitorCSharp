using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDB.SQLDB
{
    public class TableQueryObj
    {        
        public static TableQueryObj Create(CommandType inCommandType, string inCommandString)
        {
            return new TableQueryObj()
            {
                CommandType = inCommandType,
                CommandString = inCommandString,
                BindParamtersFunc = () => {},
                LoaderFunc = (dataReader, resultId) => {},
                SetOutputValueFunc = (outputValues) => {},
            };
        }
        public static TableQueryObj Create(CommandType inCommandType, string inCommandString, Action<DbDataReader, int> inLoaderFunc)
        {
            return new TableQueryObj()
            {
                CommandType = inCommandType,
                CommandString = inCommandString,
                BindParamtersFunc = () => { },
                LoaderFunc = inLoaderFunc,
                SetOutputValueFunc = (outputValues) => { },
            };
        }
        public CommandType CommandType { get; private set; }
        public string CommandString { get; private set; }

        public Action BindParamtersFunc { get; private set; } 
        public Action<DbDataReader, int> LoaderFunc { get; private set; }
        public Action<IReadOnlyDictionary<string, object>> SetOutputValueFunc { get; private set; }

    }
}

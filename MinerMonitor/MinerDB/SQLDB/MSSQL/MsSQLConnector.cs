using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDatabase.SQLDB.MSSQL
{
    public class MsSQLConnector : IDbConnector, IDisposable
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public bool HasRow { get; private set; } = false;
        public int AffectedRowCount { get; private set; } = 0;

        private Dictionary<string, object> _inputParameters = new Dictionary<string, object>();
        private List<SqlParameter> outputParameters = new List<SqlParameter>();
        public IReadOnlyDictionary<string, object> Outputs => outputParameters.ToDictionary(kv => kv.ParameterName, kv => kv.Value);
        

        public string ErrorMessage { get; private set; }

        public MsSQLConnector()
        {
        }

        public void Connect(string ip, int port, string dbName, string user, string password, int minPoolSize = 1, int maxPoolSize = 100)
        {
            _connection = new SqlConnection($"server={ip},{port};database={dbName};uid={user};pwd={password};Min Pool Size={minPoolSize};Max Pool Size={maxPoolSize}");
            var openTask = _connection.OpenAsync();
            openTask.Wait();

# if DEBUG
            Console.WriteLine($"MsSQLConnector create. pool={System.Threading.Interlocked.Increment(ref MsSQLConnectorFactory.ConnectionCount)}");
#endif
        }

        public async Task ConnectAsync(string ip, int port, string dbName, string user, string password, int minPoolSize = 1, int maxPoolSize = 100)
        {
            _connection = new SqlConnection($"server={ip},{port};database={dbName};uid={user};pwd={password};Min Pool Size={minPoolSize};Max Pool Size={maxPoolSize}");
            await _connection.OpenAsync();

# if DEBUG
            Console.WriteLine($"MsSQLConnector create. pool={System.Threading.Interlocked.Increment(ref MsSQLConnectorFactory.ConnectionCount)}");
#endif
        }


        public void SetCommand(CommandType commandType, string queryString)
        {
            _command = _connection.CreateCommand();

            _command.CommandType = commandType;
            _command.CommandText = queryString;

            //_command.Prepare();
        }

        public void BindParameter(string name, object value, ParameterDirection direction)
        {
            if (direction != ParameterDirection.Input && direction != ParameterDirection.InputOutput)
                throw new ArgumentException($"invalid direction. {direction}");

            var parameter = new SqlParameter(name, value);
            parameter.Direction = direction;

            _command.Parameters.Add(parameter);

            // [jhseo (2022.11.09)] DB Parameter 로그 처리 시 파라미터가 NULL이면 로그 파라미터에서 제외
            if (value != null)
                _inputParameters.Add(name, value);


            if (direction == ParameterDirection.InputOutput)
            {
                outputParameters.Add(parameter);
            }
        }

        public void BindOutputParameter(string name, SqlDbType dbType)
        {
            var parameter = new SqlParameter(name, dbType);
            parameter.Direction = ParameterDirection.Output;

            _command.Parameters.Add(parameter);
            outputParameters.Add(parameter);
        }

        public bool Execute(Action<DbDataReader, int> rowReadAction)
        {
            try
            {
                using (var reader = _command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    int resultSetID = 0;
                    do
                    {
                        HasRow = reader.HasRows;
                        AffectedRowCount = reader.RecordsAffected;

                        if (HasRow && rowReadAction != null)
                        {
                            while (reader.Read())
                            {
                                rowReadAction(reader, resultSetID);
                            }
                        }
                        resultSetID++;
                    } while (reader.NextResult());                    
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            finally
            {
                _command?.Dispose();
                _command = null;
                _connection?.Dispose();
                _connection = null;
# if DEBUG
                Console.WriteLine($"MsSQLConnector close. pool={System.Threading.Interlocked.Decrement(ref MsSQLConnectorFactory.ConnectionCount)}"); ;
#endif
            }
        }

        public async ValueTask<bool> ExecuteAsync(Action<DbDataReader, int> rowReadAction)
        {
            try
            {
                using (var reader = await _command.ExecuteReaderAsync())
                {
                    int resultSetID = 0;
                    do
                    {
                        HasRow = reader.HasRows;
                        AffectedRowCount = reader.RecordsAffected;
                        if (HasRow && rowReadAction != null)
                        {
                            while (await reader.ReadAsync())
                            {
                                rowReadAction(reader, resultSetID);
                            }
                        }
                        resultSetID++;
                    } while (await reader.NextResultAsync());
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            finally
            {
                _command?.Dispose();
                _command = null;
                _connection?.Dispose();
                _connection = null;
# if DEBUG
                Console.WriteLine($"MsSQLConnector close. pool={System.Threading.Interlocked.Decrement(ref MsSQLConnectorFactory.ConnectionCount)}"); ;
#endif
            }


        }

        //private void ShowPerfCounter()
        //{
        //    //string processName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        //    var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
        //    string instanceName = string.Format("{0}[{1}]", currentProcess.ProcessName, currentProcess.Id);
        //
        //    // .NET Data Provider for SqlServer 카테고리 안의
        //    // NumberOfPooledConnections 카운터 측정
        //    var counter1 = new System.Diagnostics.PerformanceCounter(".NET Data Provider for SqlServer", "NumberOfPooledConnections", instanceName);
        //    var v1 = counter1.NextValue();
        //    Console.WriteLine(v1);
        //}

        public void Dispose()
        {
            _command?.Dispose();
            _connection?.Dispose();
            _command = null;
            _connection = null;

            //ShowPerfCounter();
        }

        public string GetParameterToString()
        {
            return string.Join(",", _inputParameters.Select(kvp => kvp.ToString()));
        }
    }
}

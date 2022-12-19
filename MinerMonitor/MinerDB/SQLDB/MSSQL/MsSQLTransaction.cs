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

    public class MsSQLTransaction : IDbTransactionConnector, IDisposable
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlTransaction _transaction;

        public string ErrorMessage { get; private set; } = string.Empty;

        public MsSQLTransaction()
        {
        }

        public void Connect(string ip, int port, string dbName, string user, string password, int minPoolSize, int maxPoolSize)
        {
            _connection = new SqlConnection($"server={ip},{port};database={dbName};uid={user};pwd={password};Min Pool Size={minPoolSize};Max Pool Size={maxPoolSize}");
            _connection.Open();
# if DEBUG
            Console.WriteLine($"MsSQLTransaction create. pool={System.Threading.Interlocked.Increment(ref MsSQLConnectorFactory.ConnectionCount)}");
#endif
        }

        public async Task ConnectAsync(string ip, int port, string dbName, string user, string password, int minPoolSize, int maxPoolSize)
        {
            _connection = new SqlConnection($"server={ip},{port};database={dbName};uid={user};pwd={password};Min Pool Size={minPoolSize};Max Pool Size={maxPoolSize}");
            await _connection.OpenAsync();
# if DEBUG
            Console.WriteLine($"MsSQLTransaction create. pool={System.Threading.Interlocked.Increment(ref MsSQLConnectorFactory.ConnectionCount)}");
#endif
        }


        public void BindParameter(string name, object value)
        {
            var parameter = new SqlParameter(name, value);
            parameter.Direction = System.Data.ParameterDirection.Input;

            _command.Parameters.Add(parameter);
        }

        public void BindOutputParameter(string name, SqlDbType dbType)
        {
            var parameter = new SqlParameter(name, dbType);
            parameter.Direction = System.Data.ParameterDirection.Output;

            _command.Parameters.Add(parameter);
        }

        private bool execute(Action<DbCommand> action)
        {
            _transaction?.Dispose();
            _command?.Dispose();

            _command = _connection.CreateCommand();
            _transaction = _connection.BeginTransaction();

            _command.Connection = _connection;
            _command.Transaction = _transaction;

            try
            {
                action(_command);
                _transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    _transaction.Rollback();
                }
                catch (Exception rollbackEx)
                {
                    ErrorMessage += rollbackEx.Message;
                }

                ErrorMessage += ex.Message;
                return false;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
                _command?.Dispose();
                _command = null;
                _connection?.Dispose();
                _connection = null;

# if DEBUG
                Console.WriteLine($"MsSQLTransaction close. pool={System.Threading.Interlocked.Decrement(ref MsSQLConnectorFactory.ConnectionCount)}"); ;
#endif
            }
        }


        /*
        public bool Execute(List<ISqlTransactionQuery> queries)
        {
            return Execute((command) => {
                foreach (var query in queries)
                {
                    command.CommandText = query.CommandText;
                    if (string.IsNullOrEmpty(command.CommandText))
                        continue;

                    command.Parameters.Clear();
                    query.BindParamter(this);

                    command.ExecuteNonQuery();
                }
            });
        }

        public bool Execute(params ISqlTransactionQuery[] queries)
        {
            return Execute((command) => {
                foreach (var query in queries)
                {
                    command.CommandText = query.CommandText;
                    if (string.IsNullOrEmpty(command.CommandText))
                        continue;

                    command.Parameters.Clear();
                    query.BindParamter(this);

                    command.ExecuteNonQuery();
                }
            });
        }
        */

        public bool Execute(List<SqlTransactionQueryInfo> queryInfos)
        {
            return execute((command) => {
                foreach (var q in queryInfos)
                {
                    if (q.Queries.Count == 0)
                        continue;

                    if (string.IsNullOrEmpty(q.CommandText))
                        continue;

                    command.CommandType = q.CommandType;
                    command.CommandText = q.CommandText;

                    foreach (var obj in q.Queries)
                    {
                        // [jhseo] default로 none 값 대입, 중간에서 catch로 빠지면 이후 전체 미 실행(NONE)
                        DBResult result = DBResult.NONE;
                        string resultMsg = string.Empty;
                        try
                        {
                            command.Parameters.Clear();
                            obj.ClearParameter();
                            obj.BindParameter(this);

                            if (obj.IsScalar)
                            {
                                object retObj = command.ExecuteScalar();
                                obj.ExecuteScalarAfterEvent?.Invoke(retObj);
                            }
                            else
                            {
                                command.ExecuteNonQuery();
                                if (!obj.SetOutputParameter(command.Parameters))
                                    throw new Exception($"sql return error={obj.ReturnCode}");
                                
                            }
                            result = DBResult.SUCCESS;
                            resultMsg = string.Empty;
                        }
                        catch (Exception e)
                        {
                            result = DBResult.FAIL;
                            resultMsg = e.Message;

                            // [jhseo] 상위 try-catch에서 exception을 발생시키도록 throw
                            throw new Exception(resultMsg);
                        }
                        finally
                        {
                            // [jhseo] 각 쿼리 오브젝트 결과값 업데이트
                            obj.UpdateExcuteResult(result, resultMsg);
                        }
                    }
                }
            });
        }

        /*
        public async Task<bool> ExecuteAsync(Dictionary<string, List<ISqlTransactionQuery>> queries)
        {
            _transaction?.Dispose();
            _command?.Dispose();

            _command = _connection.CreateCommand();
            _transaction = _connection.BeginTransaction();

            _command.Connection = _connection;
            _command.Transaction = _transaction;

            try
            {

                foreach (var q in queries)
                {
                    _command.CommandText = q.Key;
                    if (string.IsNullOrEmpty(_command.CommandText))
                        continue;

                    foreach (var obj in q.Value)
                    {
                        _command.Parameters.Clear();
                        obj.BindParamter(this);

                        await _command.ExecuteNonQueryAsync();
                    }
                }

                _transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    _transaction.Rollback();
                }
                catch (Exception rollbackEx)
                {
                    ErrorMessage += rollbackEx.Message;
                }

                ErrorMessage += ex.Message;
                return false;
            }
        }
        */


        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;
            _command?.Dispose();
            _command = null;
            _connection?.Dispose();
            _connection = null;

            //GC.SuppressFinalize(this);
        }

    }
}

using DbReader.Interfaces;
using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DbReader.Concrete
{
    internal class DefaultDbReader : IDbReader
    {

        private readonly SqlConnection _connection;
        private bool _isDisposed = false;

        public DefaultDbReader(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string cannot be empty", nameof(connectionString));
            }

            _connection = new SqlConnection(connectionString);
        }

        public IEnumerable<T> Exec<T>(string query,
                                      Dictionary<string, string> parameters = null,
                                      CommandType type = CommandType.StoredProcedure) where T : class, new()
        {

            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cannot be empty", nameof(query));
            }

            List<T> result = new List<T>();

            SqlCommand command = new SqlCommand(query, _connection);

            command.CommandType = type;

            if (parameters != null && parameters.Count > 0)
            {
                SqlParameter[] sqlParameters = GetParameters(parameters);
                command.Parameters.AddRange(sqlParameters);
            }

            try
            {
                _connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        T entry = ReadEntry<T>(reader);
                        result.Add(entry);
                    }
                }

            _connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #region disposable

        ~DefaultDbReader()
        {
            if (!_isDisposed)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }

            _connection.Dispose();

            GC.SuppressFinalize(this);

            _isDisposed = true;
        }
        #endregion disposable

        #region helpers
        private T ReadEntry<T>(SqlDataReader reader) where T : class, new()
        {
            Type type = typeof(T);
            TypeAccessor accessor = TypeAccessor.Create(type);
            MemberSet members = accessor.GetMembers();

            T entry = new T();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (!reader.IsDBNull(i))
                {
                    string fieldName = reader.GetName(i);

                    if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        accessor[entry, fieldName] = reader.GetValue(i);
                    }
                }
            }
            return entry;
        }


        private SqlParameter[] GetParameters(Dictionary<string, string> parameters)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            foreach (KeyValuePair<string, string> pair in parameters)
            {
                SqlParameter parameter = new SqlParameter()
                {
                    ParameterName = $"@{pair.Key}",
                    Value = pair.Value
                };

                sqlParameters.Add(parameter);
            }

            return sqlParameters.ToArray();
        }
        #endregion helpers
    }
}

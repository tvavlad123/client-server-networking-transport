using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using log4net;

namespace Transport.Repository
{
    public class DBUtils
    {
        private static IDbConnection _connection;

        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IDbConnection GetConnection(IDictionary<string, string> props)
        {
            Logger.Debug("Get connection");
            if (_connection != null && _connection.State != ConnectionState.Closed) return _connection;
            _connection = GetNewConnection(props);
            _connection.Open();
            return _connection;
        }

        public static IDbDataParameter GetParameter(IDbCommand command, Tuple<string, object> param)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = param.Item1;
            parameter.Value = param.Item2;
            return parameter;
        }

        public static IDictionary<string, string> GetProperties()
        {
            return new Dictionary<string, string>
            {
                { "connectionString", ConfigurationManager.ConnectionStrings["transportDB"].ConnectionString}
            };
        }

        private static IDbConnection GetNewConnection(IDictionary<string, string> props)
        {
            return new SQLiteConnection(props["connectionString"]);
        }
    }
}

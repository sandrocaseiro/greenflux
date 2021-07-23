using Microsoft.Data.Sqlite;
using System.Data;

namespace Greenflux.Data
{
    public class SQLiteConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;

        public SQLiteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var dbConnection = new SqliteConnection(_connectionString);

            return dbConnection;
        }
    }
}
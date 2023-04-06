using Microsoft.Data.SqlClient;
using Projects.Infrastructure.Gateway;
using System.Data;

namespace Projects.Infrastructure
{
    public class DbConnectionBuilder : IDbConnectionBuilder
    {
        private readonly string _connectionString;
        public DbConnectionBuilder(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}

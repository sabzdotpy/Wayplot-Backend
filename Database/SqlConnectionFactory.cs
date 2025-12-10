using Microsoft.Data.SqlClient;


namespace Wayplot_Backend.Database
{
    public sealed class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _cs;
        public SqlConnectionFactory(IConfiguration config)  
        {
            _cs = config.GetConnectionString("Default")!;
        }

        public async Task<SqlConnection> Create()
        {
            var conn = new SqlConnection(_cs);
            await conn.OpenAsync();
            return conn;
        }
    }
}

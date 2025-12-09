using Microsoft.Data.SqlClient;

namespace Wayplot_Backend.Database
{
    public interface IDbConnectionFactory
    {
        Task<SqlConnection> Create();
    }
}

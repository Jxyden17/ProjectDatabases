using Microsoft.Extensions.Configuration;

namespace ProjectDatabases.Repositories
{
    public abstract class ConnectionDatabase
    {
        protected readonly string? _connectionString;

        protected ConnectionDatabase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProjectDatabase");
        }
    }
}
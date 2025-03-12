using ProjectDatabases.Models;
using Microsoft.Data.SqlClient;

namespace ProjectDatabases.Repositories
{
    public class DbActivityRepository : IActivityRepository
    {
        private readonly string? _connectionString;

        public DbActivityRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WhatsUpDatabase");
        }

        public void Add(Activity activity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Activity activity)
        {
            throw new NotImplementedException();
        }

        public List<Activity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Activity? GetById(int ActivityId)
        {
            throw new NotImplementedException();
        }

        public void Update(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}

using ProjectDatabases.Models;
using Microsoft.Data.SqlClient;
using ProjectDatabases.Controllers;

namespace ProjectDatabases.Repositories
{
    public class DbActivityRepository : IActivityRepository
    {
        private readonly string? _connectionString;

        public DbActivityRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WhatsUpDatabase");
        }

        private Activity ReadActivity(SqlDataReader reader)
        {
            // Retrieve data from fields
            int activityId = (int)reader["activity_id"];
            string activityName = (string)reader["name"];
            DateTime startTime = reader.GetDateTime(reader.GetOrdinal("start_time"));
            DateTime endTime = reader.GetDateTime(reader.GetOrdinal("end_time"));

            // Return new Acivity object
            return new Activity(activityId, activityName, startTime, endTime);
        }

        public List<Activity> GetAll()
        {
            List<Activity> activities = new();

            //1. Create an SQL connection with a connection string
            using (SqlConnection connection = new(_connectionString))
            {
                // 2. Create an SQL command with a query
                string query = "SELECT activity_id, name, start_time, end_time FROM ACTIVITY ORDER BY start_time ASC";
                SqlCommand command = new(query, connection);

                // 3. Open the SQL connection
                command.Connection.Open();

                // 4. Execute SQL command
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Activity activity = ReadActivity(reader);
                    activities.Add(activity);
                }
                reader.Close();
            }
            return activities;
        }

        public Activity? GetById(int activityId)
        {
            Activity activity = new Activity();

            // 1. Create an SQL connection with a connection string
            using (SqlConnection connection = new(_connectionString))
            {
                // 2. Create an SQL command with a query
                string query = "SELECT activity_id, name, start_time, end_time FROM ACTIVITY WHERE activity_id = @ActivityId";
                SqlCommand command = new(query, connection);
                
                // Prevent SQL injection
                command.Parameters.AddWithValue("@ActivityId", activityId);

                // 3. Open the SQL connection
                command.Connection.Open();

                // 4. Execute SQL command
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    activity = ReadActivity(reader);
                }
                reader.Close();
            }
            return activity;
        }

        public void Add(Activity activity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Activity activity)
        {
            throw new NotImplementedException();
        }


        public void Update(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}

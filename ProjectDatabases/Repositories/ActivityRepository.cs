using ProjectDatabases.Models;
using Microsoft.Data.SqlClient;
using ProjectDatabases.Controllers;

namespace ProjectDatabases.Repositories
{
<<<<<<< HEAD
    public class ActivityRepository : ConnectionDatabase , IActivityRepository 
=======
    public class ActivityRepository : ConnectionDatabase, IActivityRepository
>>>>>>> 41fe9b77fd93e755953424d2751d0f8eebd5d2b2
    {
        public ActivityRepository(IConfiguration configuration)
            : base(configuration)
        {
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
                string query = @"SELECT activity_id, [name], start_time, end_time
                                 FROM Activity
                                 ORDER BY start_time ASC;";
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
        public List<Activity> Search(string searchInput)
        {
            List<Activity> activities = new();

            //1. Create an SQL connection with a connection string
            using (SqlConnection connection = new(_connectionString))
            {
                // 2. Create an SQL command with a query
                string query = @"SELECT activity_id, [name], start_time, end_time
                                 FROM Activity
                                 WHERE [name] LIKE @InputSearch
                                 ORDER BY start_time ASC;";
                SqlCommand command = new(query, connection);

                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@InputSearch", $"%{searchInput}%");

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
            Activity? activity = null;

            // 1. Create an SQL connection with a connection string
            using (SqlConnection connection = new(_connectionString))
            {
                // 2. Create an SQL command with a query
                string query = @"SELECT activity_id, [name], start_time, end_time
                                 FROM Activity
                                 WHERE activity_id = @ActivityId;";
                SqlCommand command = new(query, connection);

                // Add parameters to prevent SQL injection
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
            // 1. Create an SQL connection with a connection string
            using (SqlConnection connection = new(_connectionString))
            {
                // 2. Create an SQL command with a query
                // Duplicates on [name] are prevented with a UNIQUE constraint in the SQL Database design
                string query = @"INSERT INTO Activity ([name], start_time, end_time) VALUES (@ActivityName, @StartTime, @EndTime);
                                 SELECT SCOPE_IDENTITY();";
                SqlCommand command = new(query, connection);

                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
                command.Parameters.AddWithValue("@StartTime", activity.StartTime);
                command.Parameters.AddWithValue("@EndTime", activity.EndTime);

                // 3. Open the SQL connection
                connection.Open();

                // 4. Execute the SQL command
                // The activity ID is an auto-incremented column, so it is returned from SQL to the Activity object.
                activity.ActivityId = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public void Update(Activity activity)
        {
            // 1. Create an SQL connection with a connection string
            using (SqlConnection connection = new(_connectionString))
            {
                // 2. Create an SQL command with a query
                string query = @"UPDATE Activity SET [name] = @ActivityName, start_time = @StartTime, end_time = @EndTime
                                 WHERE activity_id = @ActivityId;";
                SqlCommand command = new(query, connection);

                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@ActivityId", activity.ActivityId);
                command.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
                command.Parameters.AddWithValue("@StartTime", activity.StartTime);
                command.Parameters.AddWithValue("@EndTime", activity.EndTime);

                // 3. Open the SQL connection
                command.Connection.Open();

                // 4. Execute the SQL command (ExecuteNonQuery returns the number of rows affected)
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No records updated!");
            }
        }

        public void Delete(Activity activity)
        {
            // 1. Create an SQL connection with a connection string
            using (SqlConnection connection = new(_connectionString))
            {
                // 2. Create an SQL command with a query
                string query = @"DELETE FROM Activity WHERE activity_id = @ActivityId;";
                SqlCommand command = new(query, connection);

                // Prevent SQL injection
                command.Parameters.AddWithValue("@ActivityId", activity.ActivityId);

                // 3. Open the connection
                command.Connection.Open();

                // 4. Execute the SQL command
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No records deleted!");
            }
        }
    }
}
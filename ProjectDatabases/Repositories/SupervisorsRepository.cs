using ProjectDatabases.Models;
using Microsoft.Data.SqlClient;

namespace ProjectDatabases.Repositories
{
    public class SupervisorsRepository : ConnectionDatabase , ISupervisorsRepository
    {
        public SupervisorsRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        private Supervisors ReadSupervisor(SqlDataReader reader)
        {
            int teacherId = (int)reader["teacher_id"];
            int activityId = (int)reader["activity_id"];
  
            return new Supervisors(teacherId, activityId);
        }

        public List<Supervisors> GetAll()
        {
            List<Supervisors> supervisors = new List<Supervisors>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT teacher_id, activity_id FROM SUPERVISORS";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Supervisors supervisor = ReadSupervisor(reader);
                    supervisors.Add(supervisor);
                }   
                reader.Close();
            }
            return supervisors;
        }

        public Supervisors? GetById(int teacherId, int activityId)
        {
            Supervisors? supervisor = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT teacher_id, activity_id FROM SUPERVISORS WHERE teacher_id = @teacherId AND activity_id = @activityId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@teacherId", teacherId);
                command.Parameters.AddWithValue("@activityId", activityId);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    supervisor = ReadSupervisor(reader);
                }
                return null;
            }
            
        }

        public List<Teacher> GetSupervisorsByActivity(int activityId)
        {
            List<Teacher> teachers = new List<Teacher>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT TEACHER.teacher_id, TEACHER.first_name, TEACHER.last_name
                    FROM TEACHER
                    JOIN SUPERVISOR ON TEACHER.teacher_id = SUPERVISOR.teacher_id
                    WHERE SUPERVISOR.activity_id = @activityId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@activityId", activityId);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    teachers.Add(new Teacher
                    {
                        TeacherId = (int)reader["teacher_id"],
                        FirstName = (string)reader["first_name"],
                        LastName = (string)reader["last_name"]
                    });
                }
            }
            return teachers;
        }

        public List<Teacher> GetNonSupervisorsByActivity(int activityId)
        {
            List<Teacher> teachers = new List<Teacher>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT TEACHER.teacher_id, TEACHER.first_name,TEACHER.last_name
                FROM TEACHER
                LEFT JOIN SUPERVISOR 
                ON TEACHER.teacher_id = SUPERVISOR.teacher_id 
                AND SUPERVISOR.activity_id = @activityId
                WHERE SUPERVISOR.teacher_id IS NULL";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@activityId", activityId);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Teacher teacher = new Teacher
                    {
                        TeacherId = (int)reader["teacher_id"],
                        FirstName = (string)reader["first_name"],
                        LastName = (string)reader["last_name"]
                    };

                    teachers.Add(teacher);
                }

                reader.Close();
            }

            return teachers;
        }

        public void AddSupervisor(int activityId, int teacherId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO SUPERVISOR (activity_id, teacher_id) VALUES (@activityId, @teacherId)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@activityId", activityId);
                command.Parameters.AddWithValue("@teacherId", teacherId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void RemoveSupervisor(int activityId, int teacherId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {   
                string query = "DELETE FROM SUPERVISOR WHERE activity_id = @activityId AND teacher_id = @teacherId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@activityId", activityId);
                command.Parameters.AddWithValue("@teacherId", teacherId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

      
    }

    
}

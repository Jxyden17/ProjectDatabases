using Microsoft.Data.SqlClient;


//using MvcWhatsUp.Models;
using ProjectDatabases.Models;
namespace ProjectDatabases.Repositories
{
    public class TeacherRepository : ConnectionDatabase,ITeacherRepository
    {
        public TeacherRepository(IConfiguration configuration)
             : base(configuration)
        {
        }

        private Teacher ReadTeacher(SqlDataReader reader)
        {
            int id = (int)reader["teacher_id"];
            int roomId = (int)reader["room_id"];
            string firstName = (string)reader["first_name"];
            string lastName = (string)reader["last_name"];
            string phoneNumber = (string)reader["phone_number"];
            int age = (int)reader["age"];



            return new Teacher(id, roomId, firstName, lastName, phoneNumber, age);
        }
        public List<Teacher> GetAll()
        {
            List<Teacher> teachers = new List<Teacher>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT teacher_id, room_id, first_name, last_name, phone_number, age FROM TEACHER ORDER BY last_name";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Teacher teacher = ReadTeacher(reader);
                    teachers.Add(teacher);
                }
                reader.Close();
            }
            return teachers;
        }

        public Teacher? GetById(int TeacherId)
        {
            Teacher teacher = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT teacher_id, room_id, first_name, last_name, phone_number, age FROM TEACHER WHERE teacher_id = @teacher_id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@teacher_id", TeacherId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    teacher = ReadTeacher(reader);
                }

                reader.Close();

                return teacher;
            }
        }

        public void Add(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO TEACHER (room_id, first_name, last_name, phone_number, age) " +
                               "VALUES (@RoomId, @firstName, @lastName, @PhoneNumber, @Age); " +
                               "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@RoomId", teacher.RoomId);
                command.Parameters.AddWithValue("@firstName", teacher.FirstName);
                command.Parameters.AddWithValue("@lastName", teacher.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", teacher.PhoneNumber);
                command.Parameters.AddWithValue("@Age", teacher.Age);

                command.Connection.Open();
                teacher.TeacherId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE TEACHER SET first_name = @firstName, last_name = @lastName, phone_number = @PhoneNumber , " + "age = @Age WHERE teacher_id = @TeacherId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeacherId", teacher.TeacherId);
                command.Parameters.AddWithValue("@firstName", teacher.FirstName);
                command.Parameters.AddWithValue("@lastName", teacher.LastName); 
                command.Parameters.AddWithValue("@PhoneNumber", teacher.PhoneNumber);
                command.Parameters.AddWithValue("@Age", teacher.Age);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No records updated!");
            }
        }

        public void Delete(int TeacherId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"DELETE FROM TEACHER WHERE teacher_id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", TeacherId);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No records deleted!");
            }
        }

        public List<Teacher> Search(string inputSearch)
        {
            List<Teacher> teachers = new();

            //1. Create an SQL connection with a connection string
            using (SqlConnection connection = new(_connectionString))
            {
                // 2. Create an SQL command with a query
                string query = @"SELECT teacher_id, room_id, first_name, last_name, phone_number, age
                                 FROM TEACHER
                                 WHERE last_name LIKE @InputSearch
                                 ORDER BY last_name ASC;";
                SqlCommand command = new(query, connection);

                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@InputSearch", $"%{inputSearch}%");

                // 3. Open the SQL connection
                command.Connection.Open();

                // 4. Execute SQL command
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Teacher teacher = ReadTeacher(reader);
                    teachers.Add(teacher);
                }
                reader.Close();
            }
            return teachers;
        }
    }
}

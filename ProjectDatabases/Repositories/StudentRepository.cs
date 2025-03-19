using Microsoft.Data.SqlClient;
using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public class StudentRepository : ConnectionDatabase , IStudentRepository
    {
        public StudentRepository(IConfiguration configuration)
           : base(configuration)
        {
        }
        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT student_number, room_id, first_name, last_name, phone_number, class FROM STUDENT ORDER BY last_name";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = ReadStudent(reader);
                    students.Add(student);
                }
                reader.Close();
            }

            return students;
        }

        private Student ReadStudent(SqlDataReader reader)
        {
            int studentNumber = (int)reader["student_number"];
            int roomId = (int)reader["room_id"];
            string firstName = (string)reader["first_name"];
            string lastName = (string)reader["last_name"];
            string phoneNumber = (string)reader["phone_number"];
            string classNumber = (string)reader["class"];

            return new Student(studentNumber,roomId, firstName, lastName, phoneNumber, classNumber);
        }

        public List<Student> Search(string inputSearch)
        {
            List<Student> students = new();

            //1. Create an SQL connection with a connection string
            using (SqlConnection connection = new(_connectionString))
            {
                // 2. Create an SQL command with a query
                string query = @"SELECT student_number, room_id, first_name, last_name, phone_number, class
                                 FROM STUDENT
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
                    Student student = ReadStudent(reader);
                    students.Add(student);
                }
                reader.Close();
            }
            return students;
        }

        public Student? GetById(int studentId)
        {
            Student? student = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT student_number, room_id, first_name, last_name, phone_number, class FROM STUDENT WHERE student_number = @studentId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentId", studentId);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    student = ReadStudent(reader);
                }
                reader.Close();
            }
            return student;
        }

        public void Add(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO STUDENT ( room_id, first_name, last_name, phone_number, class) " +
                               "VALUES (@RoomId, @FirstName, @Lastname, @PhoneNumber, @ClassNumber); " +
                               "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@RoomId", student.RoomId);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("@ClassNumber", student.ClassNumber);


                command.Connection.Open();
                student.StudentNumber = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Update(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE STUDENT SET room_id = @RoomId, first_name = @firstName, last_name = @lastName, phone_number = @phoneNumber, class= @ClassNumber " +
                               "WHERE student_number = @studentNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentNumber", student.StudentNumber);
                command.Parameters.AddWithValue("@RoomId", student.RoomId);
                command.Parameters.AddWithValue("@firstName", student.FirstName);
                command.Parameters.AddWithValue("@lastName", student.LastName);
                command.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("@ClassNumber", student.ClassNumber);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No records updated!");
            }
        }
    

        public void Delete(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"DELETE FROM STUDENT WHERE student_number = @studentNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentNumber", student.StudentNumber);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No records deleted!");
            }
        }
    }
}

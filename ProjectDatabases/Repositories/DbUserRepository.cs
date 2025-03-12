using MvcWhatsUp.Models;
using Microsoft.Data.SqlClient;

namespace MvcWhatsUp.Repositories
{
    public class DbUserRepository : IUserRepository
    {
        private readonly string? _connectionString;

        public DbUserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WhatsUpDatabase");
        }
        public void Add(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO Users (UserName, MobileNumber, EmailAddress, Password) " +
                               "VALUES (@Name, @MobileNumber, @EmailAddress, @Password); " +
                               "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", user.UserName);
                command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAdress);
                command.Parameters.AddWithValue("@Password", user.Password);

                command.Connection.Open();
                user.UserId = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public void Delete(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"DELETE FROM Users WHERE UserId = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", user.UserId);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No records deleted!");
            }
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT UserId, UserName, MobileNumber, EmailAddress FROM Users";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = ReadUser(reader);
                    users.Add(user);
                }
                reader.Close();
            }

            return users;
        }

        public User ReadUser(SqlDataReader reader)
        {
            int id = (int)reader["UserId"];
            string name = (string)reader["UserName"];
            string mobileNumber = (string)reader["MobileNumber"];
            string emailAdress = (string)reader["EmailAddress"];

            return new User(id, name, mobileNumber, emailAdress);
        }

        public User? GetById(int UserId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT UserId, UserName, MobileNumber, EmailAddress FROM Users WHERE UserId = @UserId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            MobileNumber = reader.GetString(reader.GetOrdinal("MobileNumber")),
                            EmailAdress = reader.GetString(reader.GetOrdinal("EmailAddress"))
                        };
                    }
                }
            }
            return null;
        }

        public void Update(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET UserName = @Name, MobileNumber = @MobileNumber, " +
                               "EmailAddress = @EmailAddress WHERE UserId = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", user.UserId);
                command.Parameters.AddWithValue("@Name", user.UserName);
                command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAdress);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No records updated!");
            }
        }

    }
}

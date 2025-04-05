using Microsoft.Data.SqlClient;
using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public class DrinkRepository : ConnectionDatabase, IDrinkRepository
    {
        public DrinkRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Drink> GetAll()
        {
            List<Drink> drinks = new List<Drink>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT drink_id, name , isAlcoholic, stock FROM DRINK ORDER BY name";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Drink drink = ReadDrink(reader);
                    drinks.Add(drink);
                }
                reader.Close();
            }

            return drinks;
        }

        public Drink? GetById(int id)
        {
            Drink? drink = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT drink_id, name, isAlcoholic, stock FROM DRINK WHERE drink_id = @drinkId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@drinkId", id);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    drink = ReadDrink(reader);
                }
                reader.Close();
            }
            return drink;
        }

        private Drink ReadDrink(SqlDataReader reader)
        {
            int drinkId = (int)reader["drink_id"];
            string name = (string)reader["name"];
            bool isAlcoholic = (bool)reader["isAlcoholic"];
            int stock = (int)reader["stock"];

            return new Drink(drinkId, name, isAlcoholic, stock);
        }
    }
}

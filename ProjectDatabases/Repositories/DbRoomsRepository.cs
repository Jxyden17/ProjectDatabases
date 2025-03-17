using Microsoft.Data.SqlClient;
using ProjectDatabases.Models;
using System.Linq.Expressions;

namespace ProjectDatabases.Repositories
{
	public class DbRoomsRepository : IRoomsRepository
	{
		private readonly string? _connectionString;

		public DbRoomsRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("WhatsUpDatabase");
		}
		public void Add(Room room)
		{
			throw new NotImplementedException();
		}

		public void Delete(Room room)
		{
			throw new NotImplementedException();
		}

		public List<Room> GetAll()
		{
			List<Room> rooms = new List<Room>();

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "SELECT room_number, name, capacity, building FROM Room ORDER BY room_number";
				SqlCommand command = new SqlCommand(query, connection);

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Room room = ReadRoom(reader);
					rooms.Add(room);
				}
				reader.Close();
			}
				return rooms;
		}

		public Room? GetById(int room_number)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "SELECT room_number, name, capacity, building FROM Room WHERE room_number = @room_number";

				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@room_number", room_number);

				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						return new Room 
						{
							Room_number = reader.GetInt32(reader.GetOrdinal("room_number")),
							Name = reader.GetString(reader.GetOrdinal("name")),
							Capacity = reader.GetInt32(reader.GetOrdinal("capacity")),
							Building = reader.GetString(reader.GetOrdinal("building"))
						};
					}
				}

			}
			return null;
		}

		public void Update(Room room)
		{
			throw new NotImplementedException();
		}

		private Room ReadRoom(SqlDataReader reader)
		{
			int room_number = (int)reader["room_number"];
			string name = (string)reader["name"];
			int capacity = (int)reader["capacity"];
			string building = (string)reader["building"];

			return new Room(room_number, name, capacity, building);
		}
	}
}

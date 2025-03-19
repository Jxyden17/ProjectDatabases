using Microsoft.Data.SqlClient;
using ProjectDatabases.Models;
using System.Linq.Expressions;

namespace ProjectDatabases.Repositories
{
	public class RoomsRepository : IRoomsRepository
	{
		private readonly string? _connectionString;

		public RoomsRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("WhatsUpDatabase");
		}
		public void Add(Room room)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = @"INSERT INTO Room (room_number, name, capacity, building)
								 VALUES (@room_number, @name, @capacity, @building);
								 SELECT SCOPE_IDENTITY();";
				SqlCommand command = new SqlCommand(query, connection);

				command.Parameters.AddWithValue("@room_number", room.RoomNumber);
				command.Parameters.AddWithValue("@name", room.Name);
				command.Parameters.AddWithValue("@capacity", room.Capacity);
				command.Parameters.AddWithValue("@building", room.Building);

				command.Connection.Open();

				room.RoomNumber = Convert.ToInt32(command.ExecuteScalar());
			}
		}

		public void Delete(Room room)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = $"DELETE FROM Room WHERE room_number = @room_number";
				SqlCommand command  = new SqlCommand(query , connection);
				command.Parameters.AddWithValue("@room_number", room.RoomNumber);

				command.Connection.Open();
				int nrOfRowsAffected = command.ExecuteNonQuery();
				if (nrOfRowsAffected == 0) 
					throw new Exception("No Records Deleted");
			}
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

		public Room? GetById(int roomNumber)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = @"SELECT room_number, name, capacity, building FROM Room WHERE room_number = @room_number";

				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@room_number", roomNumber);

				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						return new Room
						{
							RoomNumber = reader.GetInt32(reader.GetOrdinal("room_number")),
							Name = reader.GetString(reader.GetOrdinal("name")),
							Capacity = reader.GetInt32(reader.GetOrdinal("capacity")),
							Building = reader.GetString(reader.GetOrdinal("building"))
						};
					}

				}

			}
			return null;
		}

		public void Edit(Room room)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "UPDATE Room SET name = @name, capacity = @capacity," +
								"building = @building WHERE room_number = @room_number";
				SqlCommand command = new SqlCommand(query, connection);

				command.Parameters.AddWithValue("@room_number", room.RoomNumber);
				command.Parameters.AddWithValue("@name", room.Name);
				command.Parameters.AddWithValue("@capacity", room.Capacity);
				command.Parameters.AddWithValue("@building", room.Building);

				connection.Open();
				int nrOfRowsAffected = command.ExecuteNonQuery();
				if (nrOfRowsAffected == 0)
				{
					throw new Exception("No records updated");
				}
			}
		}

		private Room ReadRoom(SqlDataReader reader)
		{
			int roomNumber = (int)reader["room_number"];
			string name = (string)reader["name"];
			int capacity = (int)reader["capacity"];
			string building = (string)reader["building"];

			return new Room(roomNumber, name, capacity, building);
		}
	}
}

using Microsoft.Data.SqlClient;
using ProjectDatabases.Models;
using System.Linq.Expressions;

namespace ProjectDatabases.Repositories
{
	public class RoomsRepository : ConnectionDatabase,IRoomsRepository
	{
        public RoomsRepository(IConfiguration configuration)
            : base(configuration)
        {
        }
        public void Add(Room room)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = @"INSERT INTO Room (room_number, capacity, type)
								 VALUES (@room_number, @capacity, @type);
								 SELECT SCOPE_IDENTITY();";
				SqlCommand command = new SqlCommand(query, connection);

				command.Parameters.AddWithValue("@room_number", room.RoomNumber);
				command.Parameters.AddWithValue("@capacity", room.Capacity);
				command.Parameters.AddWithValue("@type", room.Type);

				command.Connection.Open();

				room.RoomID = Convert.ToInt32(command.ExecuteScalar());
			}
		}

		public void Delete(Room room)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = $"DELETE FROM Room WHERE room_id = @room_id";
				SqlCommand command  = new SqlCommand(query , connection);
				command.Parameters.AddWithValue("@room_id", room.RoomID);

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
				string query = "SELECT room_id, room_number, capacity, type FROM Room ORDER BY room_number";
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

		public Room? GetById(int roomId)
		{
			Room? room = null;
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "SELECT room_id, room_number, capacity, type FROM Room WHERE room_id = @room_id";

				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@room_id", roomId);


				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				
				if (reader.Read())
				{
					room = ReadRoom(reader);
				}
			}
			return room;
		}

		public void Edit(Room room)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "UPDATE Room SET room_number = @room_number, capacity = @capacity," +
								"type = @type WHERE room_id = @room_id";
				SqlCommand command = new SqlCommand(query, connection);

				command.Parameters.AddWithValue("@room_id", room.RoomID);
				command.Parameters.AddWithValue("@room_number", room.RoomNumber);
				command.Parameters.AddWithValue("@capacity", room.Capacity);
				command.Parameters.AddWithValue("@type", room.Type);

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
			int roomId = (int)reader["room_id"];
			string roomNumber = (string)reader["room_number"];
			int capacity = (int)reader["capacity"];
			string type = (string)reader["type"];


			return new Room(roomId, roomNumber, capacity, type);
		}
	}
}

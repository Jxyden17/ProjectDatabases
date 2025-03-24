namespace ProjectDatabases.Models
{
	public class Room
	{
		public int RoomID { get; set; }
		public string RoomNumber { get; set; }
		public int Capacity { get; set; }
		public string Type { get; set; }

		public Room()
		{

		}
		public Room(int roomId, string roomNumber,  int capacity, string type)
		{
			RoomID = roomId;
			RoomNumber = roomNumber;
			Capacity = capacity;
			Type = type;
		}
	}
}

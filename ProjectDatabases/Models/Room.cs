namespace ProjectDatabases.Models
{
	public class Room
	{
		public int RoomNumber { get; set; }
		public string Name { get; set; }
		public int Capacity { get; set; }
		public string Building { get; set; }

		public Room()
		{

		}
		public Room(int roomNumber, string name, int capacity, string building)
		{
			RoomNumber = roomNumber;
			Name = name;
			Capacity = capacity;
			Building = building;
		}
	}
}

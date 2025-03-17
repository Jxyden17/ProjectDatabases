namespace ProjectDatabases.Models
{
	public class Room
	{
		public int Room_number { get; set; }
		public string Name { get; set; }
		public int Capacity { get; set; }
		public string Building { get; set; }

		public Room()
		{

		}
		public Room(int room_number, string name, int capacity, string building)
		{
			Room_number = room_number;
			Name = name;
			Capacity = capacity;
			Building = building;
		}
	}
}

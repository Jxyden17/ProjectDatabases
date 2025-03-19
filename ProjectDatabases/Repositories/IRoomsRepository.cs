using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
	public interface IRoomsRepository
	{
		List<Room> GetAll();
		Room? GetById(int room_number);
		void Add(Room room);
		void Edit(Room room);
		void Delete(Room room);
	}
}

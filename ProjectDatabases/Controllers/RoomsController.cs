using Microsoft.AspNetCore.Mvc;
using ProjectDatabases.Repositories;
using ProjectDatabases.Models;

namespace ProjectDatabases.Controllers
{
	public class RoomsController : Controller
	{
		private readonly IRoomsRepository _roomRepository;

		public RoomsController(IRoomsRepository roomRepository)
		{
			_roomRepository = roomRepository;
		}
		public IActionResult Index()
		{
			List<Room> room = _roomRepository.GetAll();
			return View(room);
		}
		
	}
}

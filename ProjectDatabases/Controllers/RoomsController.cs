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

		[HttpGet]

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Room room)
		{
			try
			{
				_roomRepository.Add(room);

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ViewData["ErrorMessage"] = ex.Message;
				return View(room);
			}
		}

		
	}
}

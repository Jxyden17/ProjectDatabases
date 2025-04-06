using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProjectDatabases.Repositories;
using ProjectDatabases.Models;

namespace ProjectDatabases.Controllers
{
	public class RoomsController : Controller
	{
		private readonly IRoomsRepository _roomRepository;
		private readonly IStudentRepository _studentRepository;

		public RoomsController(IRoomsRepository roomRepository, IStudentRepository studentRepository)
		{
			_roomRepository = roomRepository;
			_studentRepository = studentRepository;
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

		[HttpGet]
		public ActionResult Edit(int? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}
			Room? room = _roomRepository.GetById((int)Id);
			return View(room);
		}
		
		[HttpPost]
		public ActionResult Edit(Room room)
		{
			try
			{
				_roomRepository.Edit(room);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ViewData["ErrorMessage"] = ex.Message;
				return View(room);
			}

		}

		[HttpGet]
		public ActionResult Delete(int? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}
			Room? room = _roomRepository.GetById((int)Id);
			return View(room);
		}


		[HttpPost]
		public ActionResult Delete(Room room)
		{
			try
			{
				_roomRepository.Delete(room);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ViewData["ErrorMessage"] = ex.Message;
				return View(room);
			}
		}

		public IActionResult DormitoryStudents(int? id)
		{
			Room? Room = _roomRepository.GetById((int)id);
			List<Student>AssignedStudents = _studentRepository.GetAssignedStudents((int)id);
			List<Student>UnassignedStudents = _studentRepository.GetUnassignedStudents((int)id);

			var viewModel = new DormitoryStudentsViewModel()
			{
				Room = Room,
				AssignedStudents = AssignedStudents,
				UnassignedStudents = UnassignedStudents
			};

			return View(viewModel);
		}

		[HttpPost]
		
		public IActionResult AddToDormitory(int roomId, int studentNumber)
		{
			try
			{
				Student? student = _studentRepository.GetById(studentNumber);
				_studentRepository.AddStudentToDormitory(studentNumber, roomId);
				TempData["Confirmation"] = $"{student.FirstName} {student.LastName} has been added to the dormitory";
				return RedirectToAction("DormitoryStudents", new { id = roomId });
			}
			catch (Exception ex)
			{
				ViewData["Error"] = ex.Message;
				return RedirectToAction("DormitoryStudents", new { id = roomId });
			}
		}

		[HttpPost]

		public IActionResult RemoveFromDormitory(int roomId, int studentNumber)
		{
			try
			{
				Student? student = _studentRepository.GetById(studentNumber);
				_studentRepository.RemoveStudentFromDormitory(studentNumber, roomId);
				TempData["Confirmation"] = $"{student.FirstName} {student.LastName} has been removed from the dormitory";
				return RedirectToAction("DormitoryStudents", new { id = roomId });
			}
			catch (Exception ex)
			{
				ViewData["Error"] = ex.Message;
				return RedirectToAction("DormitoryStudents", new { id = roomId });
			}
		}


	}
}

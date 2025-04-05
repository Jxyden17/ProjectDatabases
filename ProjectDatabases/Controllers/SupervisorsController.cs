using Microsoft.AspNetCore.Mvc;
using ProjectDatabases.Models;
using ProjectDatabases.Repositories;
using System.Collections.Generic;

namespace ProjectDatabases.Controllers
{
    public class SupervisorsController : Controller
    {
        private readonly ISupervisorsRepository _supervisorsRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly ITeacherRepository _teacherRepository;
        public SupervisorsController(ISupervisorsRepository supervisorsRepository, IActivityRepository activityRepository, ITeacherRepository teacherRepository)
        {
            _supervisorsRepository = supervisorsRepository;
            _activityRepository = activityRepository;
            _teacherRepository = teacherRepository; 
        }

        [HttpGet]
        public ActionResult Manage(int activityId)
        {

            Console.WriteLine($"Manage called with activityId: {activityId}");


            if (activityId == 0)
            {
                return BadRequest("Invalid activity ID");
            }

            Activity activity = _activityRepository.GetById(activityId);
            if (activity == null)
            {
                return NotFound();
            }

            var supervisors = _supervisorsRepository.GetSupervisorsByActivity(activityId);
            var nonSupervisors = _supervisorsRepository.GetNonSupervisorsByActivity(activityId);

            var viewModel = new SupervisorsViewModel
            {
                Activity = activity,
                Supervisors = supervisors,
                NonSupervisors = nonSupervisors
            };

            return View(viewModel);
        }

    

        [HttpPost]
        public IActionResult AddSupervisor(int activityId, int teacherId)
        {
            _supervisorsRepository.AddSupervisor(activityId, teacherId);

            Teacher? teacher = _teacherRepository.GetById(teacherId);
            TempData["Confirmation"] = $"Added lecturer {teacher.FirstName} {teacher.LastName} as supervisor.";

            return RedirectToAction("Manage", new { activityId });
        }

        [HttpPost]
        public IActionResult RemoveSupervisor(int activityId, int teacherId)
        {
            _supervisorsRepository.RemoveSupervisor(activityId, teacherId);
            Teacher? teacher = _teacherRepository.GetById(teacherId);
            TempData["Confirmation"] = $"Removed lecturer {teacher.FirstName} {teacher.LastName} from the activity.";
            return RedirectToAction("Manage", new { activityId });
        }
    }
}

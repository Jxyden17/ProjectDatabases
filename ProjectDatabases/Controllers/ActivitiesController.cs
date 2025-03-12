using Microsoft.AspNetCore.Mvc;
using ProjectDatabases.Models;
using ProjectDatabases.Repositories;

namespace ProjectDatabases.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        
        public ActivitiesController(IActivityRepository activitiesRepository) 
        {
            _activityRepository = activitiesRepository;
        }

        public IActionResult Index()
        {
            // Get all activities via repository
            List<Activity> activities = _activityRepository.GetAll();
            return View(activities);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}

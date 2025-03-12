using Microsoft.AspNetCore.Mvc;
using ProjectDatabases.Models;
using ProjectDatabases.Repositories;

namespace ProjectDatabases.Controllers
{
    public class UserController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        
        public UserController(IActivityRepository activitiesRepository) 
        {
            _activityRepository = activitiesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}

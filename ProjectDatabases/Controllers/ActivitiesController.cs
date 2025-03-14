using Microsoft.AspNetCore.Mvc;
using ProjectDatabases.Models;
using ProjectDatabases.Repositories;

namespace ProjectDatabases.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivityRepository _activitiesRepository;
        
        public ActivitiesController(IActivityRepository activitiesRepository) 
        {
            _activitiesRepository = activitiesRepository;
        }

        // No search results
        public IActionResult Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                // Get all activities via repository
                List<Activity> allActivities = _activitiesRepository.GetAll();
                return View(allActivities);
            }
            else
            {
                // Get filtered activites via repository
                List<Activity>? filteredActivities = _activitiesRepository.Search(search);
                return View(filteredActivities);
            }       
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Activity activity)
        {
            try
            {
                // Add activity via repository
                _activitiesRepository.Add(activity);

                return RedirectToAction("Index");
            }
            catch
            {
                // Something goes wrong, go back to view
                return View(activity);
            }
        }

        [HttpGet]
        // Make sure the parameter is called id in the method!
        // It references app.MapControllerRoute(); in program.cs
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get Activity via repository
            Activity? activity = _activitiesRepository.GetById((int)id);
            return View(activity);
        }

        [HttpPost]
        public IActionResult Edit(Activity activity)
        {
            try
            {
                // Update Activity via repository
                _activitiesRepository.Update(activity);

                // Go back to activity list (via Index)
                return RedirectToAction("Index");
            }
            catch
            {
                // Soemthing's wrong, go back to view with user
                return View(activity);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get activity via repository
            Activity? activity = _activitiesRepository.GetById((int)id);
            return View(activity);
        }

        [HttpPost]
        public IActionResult Delete(Activity activity)
        {
            try
            {
                // Delete activity via repository
                _activitiesRepository.Delete(activity);

                // Go back to activity list (via Index)
                return RedirectToAction("Index");
            }
            catch
            {
                // Something is wrong, go back to view with activity
                return View(activity);
            }
        }
    }
}

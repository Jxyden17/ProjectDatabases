using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
                // Return the used search string to the view
                ViewData["Search"] = search;

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
            catch (SqlException ex)
            {
                // Send error through TempData object to the view
                if (ex.Number == 2627 || ex.Number == 2601) // UNIQUE constraint violation
                {
                    TempData["ErrorMessage"] = "An activity with this name already exists. Please choose a different name.";
                }
                else
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                return View(activity);
            }
            catch (Exception ex)
            {
                // Send error through TempData object to the view
                TempData["ErrorMessage"] = ex.Message;
                return View(activity);
            }
        }

        [HttpGet]
        // Make sure the parameter is called id in the method!
        // It references app.MapControllerRoute(); in program.cs
        public IActionResult Edit(int? id)
        {
            // No id was provided 
            if (id == null)
            {
                return NotFound("Error: Please provide an activity ID to edit.");
            }

            // Get Activity via repository
            Activity? activity = _activitiesRepository.GetById((int)id);

            // No activity is provided
            if (activity == null)
            {
                return NotFound($"Error: Activity with ID {id} not found.");
            }

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
            catch (SqlException ex)
            {
                // Send error through TempData object to the view
                if (ex.Number == 2627 || ex.Number == 2601) // UNIQUE constraint violation
                {
                    TempData["ErrorMessage"] = "An activity with this name already exists. Please choose a different name.";
                }
                else
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                return View(activity);
            }
            catch (Exception ex)
            {
                // Send error through TempData object to the view
                TempData["ErrorMessage"] = ex.Message;
                return View(activity);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            // No id was provided 
            if (id == null)
            {
                return NotFound("Error: Please provide an activity ID to delete.");
            }

            // Get Activity via repository
            Activity? activity = _activitiesRepository.GetById((int)id);

            // No activity is provided
            if (activity == null)
            {
                return NotFound($"Error: Activity with ID {id} not found.");
            }

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
            catch (Exception ex)
            {
                // Send error through TempData object to the view
                TempData["ErrorMessage"] = ex.Message;

                // Something is wrong, go back to view with activity
                return View(activity);
            }
        }
    }
}

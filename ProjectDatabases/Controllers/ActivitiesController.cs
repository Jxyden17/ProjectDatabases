using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProjectDatabases.Models;
using ProjectDatabases.Repositories;

namespace ProjectDatabases.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivityRepository _activitiesRepository;
        private readonly IStudentRepository _studentRepository;

        public ActivitiesController(IActivityRepository activitiesRepository, IStudentRepository studentRepository)
        {
            _activitiesRepository = activitiesRepository;
            _studentRepository = studentRepository;
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
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // UNIQUE constraint violation)
            {
                // Send error through TempData object to the view
                TempData["ErrorMessage"] = "An activity with this name already exists. Please choose a different name.";
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
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // UNIQUE constraint violation
            {
                // Send error through TempData object to the view
                TempData["ErrorMessage"] = "An activity with this name already exists. Please choose a different name.";
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

        public IActionResult Participants(int? id)
        {
            // No id was provided 
            if (id == null)
            {
                return NotFound("Error: Please provide an activity ID to view the participants of.");
            }

            // Get Activity via repository
            ActivityParticipants? activityParticipants = new();
            activityParticipants.Activity = _activitiesRepository.GetById((int)id);
            activityParticipants.Participants = _studentRepository.GetParticipants((int)id);
            activityParticipants.NonParticipants = _studentRepository.GetNonParticipants((int)id);

            // No activity is provided
            if (activityParticipants.Activity == null)
            {
                return NotFound($"Error: Activity with ID {id} not found.");
            }

            return View(activityParticipants);
        }

        public IActionResult AddParticipant(int? id, int? student_number)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "No activity ID was given with the add request";
            }
            else if (student_number == null)
            {
                TempData["ErrorMessage"] = "No student number was given with the add request";
            }
            else //(id != null && student_number != null)
            {
                try
                {
                    _activitiesRepository.AddStudent((int)id, (int)student_number);
                    TempData["SuccessMessage"] = "Student successfully added!";
                }
                catch (SqlException ex) when (ex.Number == 547) // Foreign key constraint violation)
                {
                    TempData["ErrorMessage"] = "The student you tried to add to this activity does not exist in the students list.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }
            // RedirectToAction(name of the action, name of the controller, values for the route
            // Refreshes the entire page, causing it to scroll to the top. Need some kind of asynchronous loading / partial loading to improve UX.. :/
            return RedirectToAction("Participants", "Activities", new { id = id });
        }

        public IActionResult RemoveParticipant(int? id, int? student_number)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "No activity ID was given with the removal request";
            }
            else if (student_number == null)
            {
                TempData["ErrorMessage"] = "No student number was given with the removal request";
            }
            else //(id != null && student_number != null)
            {
                try
                {
                    _activitiesRepository.RemoveStudent((int)id, (int)student_number);
                    TempData["SuccessMessage"] = "Student successfully removed!";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }
            // RedirectToAction(name of the action, name of the controller, values for the route
            // Refreshes the entire page, causing it to scroll to the top. Need some kind of asynchronous loading / partial loading to improve UX.. :/
            return RedirectToAction("Participants", "Activities", new { id = id });
        }
    }
}

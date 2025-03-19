using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


//using MvcWhatsUp.Repositories;
using ProjectDatabases.Models;
using ProjectDatabases.Repositories;

namespace ProjectDatabases.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
   
        public IActionResult Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                // Get all activities via repository
                List<Teacher> allTeachers = _teacherRepository.GetAll();
                return View(allTeachers);
            }
            else
            {
                // Return the used search string to the view
                ViewData["Search"] = search;

                // Get filtered activites via repository
                List<Teacher>? filteredActivities = _teacherRepository.Search(search);
                return View(filteredActivities);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            try
            {

                _teacherRepository.Add(teacher);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View(teacher);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                Teacher? teacher = _teacherRepository.GetById((int)id);
                return View(teacher);
            }
            catch (Exception ex)
            {

                return View(id);
            }
           
        }

        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            try
            {
                _teacherRepository.Update(teacher);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(teacher);
            }
        }


        [HttpPost]
        public ActionResult Delete(int TeacherId)
        {
            try
            {
                _teacherRepository.Delete(TeacherId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete failed: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Teacher? teacher = _teacherRepository.GetById((int)id);
            return View(teacher);
        }




    }
}

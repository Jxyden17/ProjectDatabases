using Microsoft.AspNetCore.Mvc;
using ProjectDatabases.Models;
using ProjectDatabases.Repositories;

namespace ProjectDatabases.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IActionResult Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                // Get all activities via repository
                List<Student> allStudents = _studentRepository.GetAll();
                return View(allStudents);
            }
            else
            {
                // Return the used search string to the view
                ViewData["Search"] = search;

                // Get filtered activites via repository
                List<Student>? filteredStudents = _studentRepository.Search(search);
                return View(filteredStudents);
            }
        }

        //Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                _studentRepository.Add(student);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewData["ErrorMessage"] = ex.Message;
                return View(student);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        //Edit 
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            try
            {
                _studentRepository.Update(student);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View(student);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student? student = _studentRepository.GetById((int)id);
            return View(student);
        }

        //Delete
        [HttpPost]
        public ActionResult Delete(Student student)
        {
            try
            {
                _studentRepository.Delete(student);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(student);
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student? student = _studentRepository.GetById((int)id);
            return View(student);
        }
    }
}

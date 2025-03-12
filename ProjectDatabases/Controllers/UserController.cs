using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.Repositories;

namespace MvcWhatsUp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        
        public UserController(IUserRepository usersRepository) 
        {
            _userRepository = usersRepository;
        }

        public IActionResult Index()
        {
            
            List<User> users = _userRepository.GetAll();
            return View(users);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public string Login(string name, string password)
        {
            return $"User: {name} tried to login with password: {password}";
        }


        //Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                _userRepository.Add(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        //Edit 
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                _userRepository.Update(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            User? user = _userRepository.GetById((int)id);
            return View(user);
        }

        //Delete
        [HttpPost]
        public ActionResult Delete(User user)
        {
            try
            {
                _userRepository.Delete(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User? user = _userRepository.GetById((int)id);
            return View(user);
        }
    }
}

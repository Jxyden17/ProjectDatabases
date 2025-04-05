using Microsoft.AspNetCore.Mvc;
using ProjectDatabases.Models;
using ProjectDatabases.Repositories;
using System.Reflection;

namespace ProjectDatabases.Controllers
{
    public class OrderController : Controller
    {


        private readonly IDrinkRepository _drinkRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IDrinkRepository drinkRepository, IStudentRepository studentRepository, IOrderRepository orderRepository)
        {
            _drinkRepository = drinkRepository;
            _studentRepository = studentRepository;
            _orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            var viewModel = new Order
            {
                Drinks = _drinkRepository.GetAll(),
                Students = _studentRepository.GetAll()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(Order model)
        {
            try
            {

                // Find selected student and drink
                Student? student = _studentRepository.GetById(model.SelectedStudentId);
                Drink? drink = _drinkRepository.GetById(model.SelectedDrinkId);

                if (student == null || drink == null || model.DrinkAmount <= 0)
                {

                    model.ErrorMessage = "Select a student and drink";

                    reloadStudentAndDrinks(model);

                    return View("Index", model);
                }



                // Try to addOrder when it fails it throws and error and goes tot the catch block.
                _orderRepository.AddOrder(model);


                // Show confirmation message
                model.ConfirmationMessage = $"{student.FirstName} {student.LastName} ordered {model.DrinkAmount}x {drink.Name}";

                // Reload students and drinks for the form
                reloadStudentAndDrinks(model);

                return View(model);
            }
            catch (Exception ex)
            {
                reloadStudentAndDrinks(model);
                model.ErrorMessage = "You cant order more than the amount of drinks.";
                return View(model);
            }
        }

        public void reloadStudentAndDrinks(Order model)
        {
            model.Students = _studentRepository.GetAll();
            model.Drinks = _drinkRepository.GetAll();
        }
    }
}

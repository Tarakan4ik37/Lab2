using kurs3.Models;
using Microsoft.AspNetCore.Mvc;

namespace kurs3.Controllers
{
    public class CustomerController : Controller
    {

        private readonly Kyrsach2Context _kyrsach2Context;

        public CustomerController(Kyrsach2Context kyrsach2Context)
        {
            _kyrsach2Context = kyrsach2Context;
        }
        public IActionResult Index()
        {
            List<Customer> customers = _kyrsach2Context.Customers.ToList();
            return View(customers); // Указываем явный путь к представлению
        }

        // Отображение формы для создания
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Customer()); // Инициализация модели если необходимо
        }

        // Обработка данных формы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customers)
        {
            _kyrsach2Context.Add(customers);
            _kyrsach2Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var customers = _kyrsach2Context.Customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }
            return View(customers);
        }

    }

}

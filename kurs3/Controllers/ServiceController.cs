using kurs3.Models;
using Microsoft.AspNetCore.Mvc;

namespace kurs3.Controllers
{
    public class ServiceController : Controller
    {
        private readonly Kyrsach2Context _kyrsach2Context;

        public ServiceController(Kyrsach2Context kyrsach2Context)
        {
            _kyrsach2Context = kyrsach2Context;
        }
        public IActionResult Index()
        {
            List<Service> services = _kyrsach2Context.Services.ToList();
            return View(services); // Указываем явный путь к представлению
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(new Service()); // Инициализация модели если необходимо
        }

        // Обработка данных формы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Service service)
        {
            _kyrsach2Context.Add(service);
            _kyrsach2Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

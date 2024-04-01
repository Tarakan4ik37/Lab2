using kurs3.Models;
using Microsoft.AspNetCore.Mvc;

namespace kurs3.Controllers
{
    public class  HostController : Controller
    {
        private readonly  Kyrsach2Context _kyrsach2Context;

        public HostController(Kyrsach2Context kyrsach2Context)
        {
            _kyrsach2Context = kyrsach2Context;
        }
        public IActionResult Index()
        {
            List<Models.Host> hosts = _kyrsach2Context.Hosts.ToList();
            return View(hosts); // Указываем явный путь к представлению
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(new Models.Host()); // Инициализация модели если необходимо
        }

        // Обработка данных формы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.Host hosts)
        {
            _kyrsach2Context.Add(hosts);
            _kyrsach2Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

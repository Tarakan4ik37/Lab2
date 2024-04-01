using kurs3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace kurs3.Controllers
{
    public class AddressController : Controller
    {
        private readonly Kyrsach2Context _kyrsach2Context;

        public AddressController(Kyrsach2Context kyrsach2Context)
        {
            _kyrsach2Context = kyrsach2Context;
        }
        public IActionResult Index()
        {
            List<Address> addresses = _kyrsach2Context.Addresses.ToList();
            return View(addresses); // Указываем явный путь к представлению
            return View(addresses);
        }
        
       
        [HttpGet]
        public IActionResult Create()
        {

            return View(new Address()); // Инициализация модели если необходимо
        }

        // Обработка данных формы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Address addresss)
        {          
            _kyrsach2Context.Add(addresss);
            _kyrsach2Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

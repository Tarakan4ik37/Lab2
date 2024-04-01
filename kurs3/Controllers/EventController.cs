using kurs3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using System.Xml.Linq;


namespace kurs3.Controllers
{
    public class EventController : Controller
    {

        private readonly Kyrsach2Context _kyrsach2Context;


        public EventController(Kyrsach2Context kyrsach2Context)
        {
            _kyrsach2Context = kyrsach2Context;
        }
        public IActionResult Index()
        {
            List<Event> events = _kyrsach2Context.Events.ToList();

            return View(events); 
            // Указываем явный путь к представлению
  
        }
        

        // Отображение формы для создания
        [HttpGet]
        public IActionResult Create()
        {

            return View(new Event()); // Инициализация модели если необходимо
        }

        // Обработка данных формы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event events)
        {
            // Загрузка стоимости из связанных таблиц
            var hostCost = _kyrsach2Context.Hosts
                .Where(h => h.HostId == events.HostId)
                .Select(h => h.ServiceCost)
                .FirstOrDefault();

            var venueCost = _kyrsach2Context.Venues
                .Where(v => v.VenueId == events.VenueId)
                .Select(v => v.RentalCost)
                .FirstOrDefault();

            var serviceCost = _kyrsach2Context.Services
                .Where(s => s.ServiceId == events.ServiceId)
                .Select(s => s.Cost)
                .FirstOrDefault();


            // Вычисление общей стоимости
            events.TotalCost = hostCost + venueCost + serviceCost;

            _kyrsach2Context.Add(events);
            _kyrsach2Context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int id)
        {

            var events = _kyrsach2Context.Events.Find(id);
            if (events == null)
            {          
                return NotFound();              
            }       
            return View(events);
        }
    }

}                                

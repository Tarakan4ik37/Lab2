
using kurs3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace kurs3.Controllers
{

    public class KyrsController : Controller
    {
        private readonly Kyrsach2Context _kyrsach2Context;

        public KyrsController(Kyrsach2Context kyrsach2Context)
        {
            _kyrsach2Context = kyrsach2Context;
        }
        public IActionResult Index()
        {
            List<Venue> venues = _kyrsach2Context.Venues.ToList();
            return View( venues); // Указываем явный путь к представлению
        }


        // Отображение формы для редактирования
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Venue venue = await _kyrsach2Context.Venues.FirstOrDefaultAsync(p => p.VenueId == id);
                if (venue != null)
                    return View( venue);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Venue venue)
        {
            _kyrsach2Context.Venues.Update(venue);
            await _kyrsach2Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // Отображение подтверждения удаления
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Venue venue = await _kyrsach2Context.Venues.FirstOrDefaultAsync(p => p.VenueId == id);
                if (venue != null)
                    return View(venue);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Venue venue = await _kyrsach2Context.Venues.FirstOrDefaultAsync(p => p.VenueId == id);
                if (venue != null)
                {
                    _kyrsach2Context.Venues.Remove(venue);
                    await _kyrsach2Context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        // Отображение формы для создания
        [HttpGet]
        public IActionResult Create()
        {
            
            return  View(new Venue()); // Инициализация модели если необходимо
        }


        // Обработка данных формы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Venue venue)
        {
            
            // Проверка URL изображения, если она необходима
            if (!string.IsNullOrWhiteSpace(venue.Photos))
            {
                if (!Uri.TryCreate(venue.Photos, UriKind.Absolute, out var uriResult)
                    || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                {
                    ModelState.AddModelError("PhotoUrl", "Указан неверный URL изображения.");
                }
            }           
            _kyrsach2Context.Add(venue);
            _kyrsach2Context.SaveChanges();
            return RedirectToAction(nameof(Index));           
        }

        public IActionResult Details(int id)
        {
            var venue = _kyrsach2Context.Venues.Find(id);
            if (venue == null)
            {
               return NotFound();
            }
            return View(venue);
        }


    }
}
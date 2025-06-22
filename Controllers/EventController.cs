using Microsoft.AspNetCore.Mvc;
using EventManagement.Data;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly EventContext _context;

        public EventController(EventContext context) => _context = context;

        public IActionResult Index()
        {
            var events = _context.Events.ToList();
            return View(events);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event ev)
        {
            _context.Events.Add(ev);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var ev = _context.Events.Find(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event ev)
        {
            _context.Events.Update(ev);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var ev = _context.Events.Find(id);
            if (ev == null) return NotFound();
            _context.Events.Remove(ev);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

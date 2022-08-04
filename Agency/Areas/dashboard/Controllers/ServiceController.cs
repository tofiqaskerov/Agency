using Agency.Data;
using Agency.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var service = _context.Services.ToList();
            return View(service);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Services service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit( int id )
        {
            var service = _context.Services.FirstOrDefault(x => x.Id == id);

            return View(service);
        }

        [HttpPost]
        public IActionResult Edit( Services services )
        {
            _context.Services.Update(services);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete( int id )
        {
            var service = _context.Services.FirstOrDefault(x => x.Id == id);
            return View(service);
        }
        [HttpPost]
        public IActionResult Delete(int id, Services service)
        {
            _context.Services.Remove(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
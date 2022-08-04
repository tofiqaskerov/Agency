using Agency.Data;
using Agency.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agency.Areas.dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;
        public PortfolioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var portfolio = _context.Portfolios.Include(x =>x.Category).ToList();
            return View(portfolio);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var category = _context.Categories.ToList();
            ViewData["Categories"] = category;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Portfolio portfolio,string category , IFormFile NewPhoto)
        {
            var fileExtation = Path.GetExtension(NewPhoto.FileName);
            if (fileExtation != ".jpg")
            {
                ViewBag.PhotoError = "Yalniz sekil formati qebul olunur";
                return View();
            }
            string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewPhoto.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photo", myPhoto);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                NewPhoto.CopyTo(stream);
            }
                
            var cat = _context.Categories.FirstOrDefault(x => x.Name == category);
            portfolio.CategoryId = cat.Id;
            portfolio.PhotoURL = "Photo/" + myPhoto;
            _context.Portfolios.Add(portfolio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

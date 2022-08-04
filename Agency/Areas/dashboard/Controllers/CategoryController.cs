using Agency.Data;
using Agency.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            var selectCategory = _context.Categories.FirstOrDefault(x =>x.Name == category.Name);
            if(selectCategory != null)
            {
                ViewBag.CategoryExist = "Category Movcuddur";
                return View();
            }
       
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete( int? id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (id == null)
                return NotFound();
            if (category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete (Category category)
        {
            
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                var portfolios = _context.Portfolios.Where(x => x.CategoryId == category.Id).ToList();
                if (portfolios != null)
                {
                    ViewBag.ExistCategory = "Bu kateqoriyada asagidaki protfolio olduguna gore sile bilmezsiniz ";
                    ViewData["portfolios"] = portfolios;
                }
                else
                {
                    ViewBag.CategoryError = "Bilinmeyen sebebden error cixdi. zehmet olmasa yeniden cehd edin";
                }
                return View();
            }

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (id == null)
                return NotFound();
            if (category == null)
                return NotFound();
            
            return View(category);

        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }
    }
}

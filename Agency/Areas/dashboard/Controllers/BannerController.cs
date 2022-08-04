using Agency.Data;
using Agency.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize]
    public class BannerController : Controller
    {
        private readonly AppDbContext _context;

        public BannerController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var banner = _context.Banners.FirstOrDefault();

            return View(banner);
        }

      
        
        public IActionResult Create()
        {
            var banner = _context.Banners.FirstOrDefault();
            if(banner != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create( Banner banner, IFormFile NewPhoto )
        {

            var fileExtation = Path.GetExtension( NewPhoto.FileName );
            if( fileExtation != ".jpg")
            {
                ViewBag.PhotoError = "Yalniz sekil formati qebul olunur";
                return View();
            }
            string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension( NewPhoto.FileName );
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photo", myPhoto);
            using(var stream = new FileStream(path, FileMode.Create))
            {
                NewPhoto.CopyTo(stream);    
            }
            banner.PhotoURL = "Photo/"+myPhoto;
            _context.Banners.Add(banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var banner = _context.Banners.FirstOrDefault(x => x.Id == id);
            if (banner == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete( Banner banner )
        {
            if(banner == null)
            {
                return RedirectToAction("Index");
            }
            _context.Banners.Remove(banner);  
            _context.SaveChanges(); 
            return RedirectToAction("Index");   
        }
        [HttpGet]
        public IActionResult Detail( int id )
        {   
            if(id == null)
            {
                return NotFound();
            }
            var banner = _context.Banners.FirstOrDefault(x =>x.Id == id);
            if(banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if( id == null)
            {
                return NotFound();
            }
            var banner = _context.Banners.FirstOrDefault(x =>x.Id ==id);
            if(banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }
        [HttpPost]
        public IActionResult Edit(Banner banner, IFormFile NewPhoto, string? oldPhoto)
        {
            if(NewPhoto != null)
            {
                var fileExtation = Path.GetExtension(NewPhoto.FileName);
                if (fileExtation != ".jpg")
                {
                    ViewBag.PhotoError = "Yalniz jpg formati qebul olunur";
                    return View();
                };
                string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewPhoto.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photo", myPhoto);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    NewPhoto.CopyTo(stream);
                }
                banner.PhotoURL = "Photo/" + myPhoto;
            }
            else
            {
                banner.PhotoURL = oldPhoto;
            }
            
            _context.Banners.Update(banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

using Agency.Data;
using Agency.Models;
using Agency.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agency.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        private readonly DataSeeding _dataSeeding;

        public HomeController(AppDbContext context, DataSeeding dataSeeding)
        {
            _context = context;
            _dataSeeding = dataSeeding;
        }

        
        public IActionResult Index()
        {
            _dataSeeding.SeedData();   
            var banner = _context.Banners.FirstOrDefault();
            var services = _context.Services.ToList();
            var abouts = _context.Abouts.ToList();
            var teams = _context.Teams.Include(x =>x.Position).ToList();
            var socials = _context.Socials.Include(x => x.SocialNetwork).ToList();
            var portfolio = _context.Portfolios.Include(x => x.Category).ToList();
            HomeVM vm = new()
            {
                Banner = banner,
                Services = services,
                Abouts = abouts,
                Socials = socials,
                Teams = teams,
                Portfolios = portfolio,
            };
            
            return View(vm);

        }
        public IActionResult About()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(Contact contact)
        {  
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

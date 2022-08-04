using Agency.Models;
using Microsoft.EntityFrameworkCore;

namespace Agency.Data
{
    public class DataSeeding
    {
        private readonly AppDbContext _context;
        public DataSeeding(AppDbContext context)
        {
            _context = context;
        }
 
        public void SeedData()
        {
            if (_context.Database.GetPendingMigrations().Count() == 0)
            {
                if (_context.Categories.Count() == 0)
                {
                    _context.Categories.AddRange(Categories);
                }
                if (_context.Portfolios.Count() == 0)
                {
                    _context.Portfolios.AddRange(Portfolios);
                }
                _context.SaveChanges(); 
            }
        }
        public static Category[] Categories =
        {
            new Category() {Name = "Illustration"},
            new Category() {Name = "Graphic Design"},
            new Category() {Name = "Website Design" }
        };

        public static Portfolio[] Portfolios =
        {
            new Portfolio() {
                Title = "Threads",
                PhotoURL = "https://ichef.bbci.co.uk/news/976/cpsprodpb/A20B/production/_123138414_1ae36bae-44c9-4277-89a0-7b41aaca2cdb.jpg",
                Description = "Lorem ipsum",
                Client = "Musteri 1",
                Category = Categories[0]
            },
            new Portfolio() {
                Title = "Explore",
                PhotoURL = "https://images-ext-2.discordapp.net/external/3jqAYUUARmCersMxf3OMPoGlV7j-iZVNMss1IevKqic/https/cdn.pixabay.com/photo/2022/05/23/13/09/grass-7216163_960_720.jpg",
                Description = "Lorem ipsum",
                Client = "Musteri 2",
                Category = Categories[1]
            },
            new Portfolio() {
                Title = "Finish",
                PhotoURL = "https://localhost:7292/assets/img/portfolio/3.jpg",
                Description = "Lorem ipsum",
                Client = "Musteri 3",
                Category = Categories[2]
            }
        };
    }
}

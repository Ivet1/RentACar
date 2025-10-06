using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Models;
using RentACar.Data;
using System.Diagnostics;
using RentAcar.Models;

namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection for logger and ApplicationDbContext
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Home/Home 
        public async Task<IActionResult> Home()
        {
            // Fetching the list of cars from the database
            var cars = await _context.Cars.ToListAsync();
            // Passing the list of cars to the Home view
            return View("Home", cars); // Returning the Home 
        }

        // Error handling (in case of exceptions)
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
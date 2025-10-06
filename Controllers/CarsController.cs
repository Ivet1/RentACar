using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAcar.Models;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.ToListAsync();
            return View(cars);
        }

        // GET: Cars/Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            var model = new CarViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Seats = car.Seats,
                Description = car.Description,
                PricePerDay = car.PricePerDay,
                IsAvailable = car.IsAvailable
            };

            return View(model);
        }

        // GET: Cars/Create
        public IActionResult Create() => View();

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var car = new Car
            {
                Brand = model.Brand,
                Model = model.Model,
                Year = model.Year,
                Seats = model.Seats,
                Description = model.Description,
                PricePerDay = model.PricePerDay,
                IsAvailable = model.IsAvailable
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            var model = new CarViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Seats = car.Seats,
                Description = car.Description,
                PricePerDay = car.PricePerDay,
                IsAvailable = car.IsAvailable
            };

            return View(model);
        }

        // POST: Cars/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarViewModel model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            car.Brand = model.Brand;
            car.Model = model.Model;
            car.Year = model.Year;
            car.Seats = model.Seats;
            car.Description = model.Description;
            car.PricePerDay = model.PricePerDay;
            car.IsAvailable = model.IsAvailable;

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            return View(car);
        }

        // POST: Cars/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

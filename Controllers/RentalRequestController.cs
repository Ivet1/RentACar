using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Controllers
{
    public class RentalRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RentalRequestsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Show available cars for given dates
        public async Task<IActionResult> AvailableCars(DateTime startDate, DateTime endDate)
        {
            if (startDate == default || endDate == default)
            {
                ModelState.AddModelError(string.Empty, "Please select valid dates.");
                return View("~/Views/AvailableCars.cshtml");
            }
            else if (startDate >= endDate)
            {
                ModelState.AddModelError(string.Empty, "End date must be later than the start date.");
                return View("~/Views/AvailableCars.cshtml");
            }

            var reservedCarIds = await _context.RentalRequests
                .Where(r => r.StartDate < endDate && r.EndDate > startDate)
                .Select(r => r.CarId)
                .Distinct()
                .ToListAsync();

            var availableCars = await _context.Cars
                .Where(c => !reservedCarIds.Contains(c.Id))
                .ToListAsync();

            if (!availableCars.Any())
            {
                ModelState.AddModelError(string.Empty, "No cars available for the selected dates.");
            }

            return View("~/Views/AvailableCars.cshtml", availableCars);
        }

        [Authorize]
        public async Task<IActionResult> CreateRequest(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null) return NotFound();
            return View("~/Views/CreateRequest.cshtml", new RentalRequest { CarId = carId });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(RentalRequest model)
        {
            if (!ModelState.IsValid) return View("~/Views/CreateRequest.cshtml", model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            bool isCarAvailable = !_context.RentalRequests.Any(r =>
                r.CarId == model.CarId &&
                r.StartDate < model.EndDate &&
                r.EndDate > model.StartDate);

            if (!isCarAvailable)
            {
                ModelState.AddModelError("", "The car is already reserved for the selected dates.");
                return View("~/Views/CreateRequest.cshtml", model);
            }

            var rentalRequest = new RentalRequest
            {
                CarId = model.CarId,
                UserId = user.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                IsApproved = false
            };

            _context.RentalRequests.Add(rentalRequest);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Rental request has been created successfully!";
            return RedirectToAction("UserRequests");
        }

        [Authorize]
        public async Task<IActionResult> UserRequests()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var requests = await _context.RentalRequests
                .Include(r => r.Car)
                .Where(r => r.UserId == user.Id)
                .ToListAsync();

            return View(requests);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PendingRequests()
        {
            var requests = await _context.RentalRequests
                .Include(r => r.Car)
                .Include(r => r.User)
                .Where(r => !r.IsApproved)
                .ToListAsync();

            if (!requests.Any())
            {
                ViewData["Message"] = "There are no pending requests to review.";
            }

            return View(requests);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            var request = await _context.RentalRequests.FindAsync(id);
            if (request == null) return NotFound();

            request.IsApproved = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(PendingRequests));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            var request = await _context.RentalRequests.FindAsync(id);
            if (request == null) return NotFound();

            _context.RentalRequests.Remove(request);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(PendingRequests));
        }
    }
}

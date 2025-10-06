using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Add this to support ICollection

namespace RentACar.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "EGN must be exactly 10 digits.")]
        public string EGN { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public ICollection<RentalRequest> RentalRequests { get; set; } = new List<RentalRequest>();
    }
}

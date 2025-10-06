using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; 

namespace RentACar.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Brand { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Model { get; set; } = null!;

        [Required]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
        public int Year { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Seats must be between 1 and 10.")]
        public int Seats { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Price must be between 0 and 1000.")]
        public decimal PricePerDay { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<RentalRequest> RentalRequests { get; set; } = new List<RentalRequest>();
    }
}

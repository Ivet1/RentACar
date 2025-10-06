using System.ComponentModel.DataAnnotations;

namespace RentAcar.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        [MaxLength(50, ErrorMessage = "Brand cannot exceed 50 characters.")]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = "Model is required.")]
        [MaxLength(50, ErrorMessage = "Model cannot exceed 50 characters.")]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = "Year is required.")]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Seats is required.")]
        [Range(1, 10, ErrorMessage = "Seats must be between 1 and 10.")]
        public int Seats { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price per day is required.")]
        [Range(0, 1000, ErrorMessage = "Price must be between 0 and 1000.")]
        public decimal PricePerDay { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}

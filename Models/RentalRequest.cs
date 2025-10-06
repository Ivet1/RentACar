using System;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class RentalRequest
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }
        public Car Car { get; set; } // Navigation property for Car

        [Required]
        public string UserId { get; set; } // Foreign key to ApplicationUser
        public ApplicationUser User { get; set; } // Navigation property for ApplicationUser

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be after the start date.")]
        public DateTime EndDate { get; set; }

        public bool IsApproved { get; set; } // Field to mark if the request is approved
    }
}

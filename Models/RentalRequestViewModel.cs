using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class RentalRequestViewModel
    {
        public int CarId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be after the start date.")]
        public DateTime EndDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models.DTOs
{
    public class ReservationDTO2
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int TableId { get; set; }
        [Required]
        public DateTime ReservationStart { get; set; }
        [Required]
        public DateTime ReservationEnd { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "Number must be between 1-8")]
        public int NoOfPeople { get; set; }
    }
}

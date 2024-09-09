using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantProject.Models.DTOs
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }
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

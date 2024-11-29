using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models.DTOs
{
    public class ReservationUpdateDTO
    {
        //public int ReservationId { get; set; }
        public int TableId { get; set; }
        [Required]
        public DateTime ReservationStart { get; set; }
        [Required]
        [Range(1, 8, ErrorMessage = "Number must be between 1-8")]
        public int NoOfPeople { get; set; }
    }
}

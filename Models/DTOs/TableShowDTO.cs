using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models.DTOs
{
    public class TableShowDTO
    {
        public int TableId { get; set; }
        [Required]
        public int NoOfSeats { get; set; }
        [Required]
        public bool Availability { get; set; }

    }
}

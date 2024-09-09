using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models.DTOs
{
    public class TableDTO2
    {
        [Required]
        public int NoOfSeats { get; set; }
        [Required]
        public bool Availability { get; set; }

    }
}

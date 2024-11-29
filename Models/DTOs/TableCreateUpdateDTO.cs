using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models.DTOs
{
    public class TableCreateUpdateDTO
    {
        [Required]
        public int NoOfSeats { get; set; }
        [Required]
        public bool Availability { get; set; }

    }
}

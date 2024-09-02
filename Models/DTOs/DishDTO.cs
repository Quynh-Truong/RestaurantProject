using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models.DTOs
{
    public class DishDTO//validate data being sent to DB is validated through d annotations
    {
        [Required]
        [StringLength(250, ErrorMessage = "Name cannot be longer than 250 letters.")]
        public string Name { get; set; }
        [Required]
        [Range(0.01, 2000.00, ErrorMessage = "Price must be between 0.01 and 2000.00.")]
        public double Price { get; set; }
        public bool Availability { get; set; }

    }
}

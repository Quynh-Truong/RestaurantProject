using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantProject.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool Availability { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(7)]
        public string PhoneNo { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}

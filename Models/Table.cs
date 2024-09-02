using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        [Required]
        public int NoOfSeats { get; set; }
        [Required]
        public bool Availability { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}

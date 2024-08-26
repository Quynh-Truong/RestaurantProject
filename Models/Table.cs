using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }
        [Required]
        public int TableNo { get; set; }
        [Required]
        public int NoOfSeats { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}

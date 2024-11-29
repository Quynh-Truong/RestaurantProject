using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantProject.Models.DTOs
{
    public class ReservationsShowDTO
    {
        public int ReservationId { get; set; }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        [Required]
        public int TableId { get; set; }
        [Required]
        public DateTime ReservationStart { get; set; }
  
        public DateTime ReservationEnd { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "Number must be between 1-8")]
        public int NoOfPeople { get; set; }




    }
}

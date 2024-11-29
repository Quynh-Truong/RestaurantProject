using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models.DTOs
{
    public class ReservationMakeDTO
    {

        public int CustomerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public int TableId { get; set; }
        [Required]
        public DateTime ReservationStart { get; set; }
      
        //public DateTime ReservationEnd { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "Number must be between 1-8")]
        public int NoOfPeople { get; set; }
        public string PhoneNo { get; set; }
    }
}

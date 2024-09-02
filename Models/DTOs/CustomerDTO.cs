using System.ComponentModel.DataAnnotations;

namespace RestaurantProject.Models.DTOs
{
    public class CustomerDTO
    {
        [Required]
        [StringLength(250, ErrorMessage = "First name cannot be longer than 250 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Last name cannot be longer than 250 characters.")]
        public string LastName { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
        public string PhoneNo { get; set; }
    }
}

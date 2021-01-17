using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp
{
    public class CustomerViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Company name must be provided")]
        public string CompanyName { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "Name must be at most {1} characters long.")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Address must be provided")]
        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        [Required(ErrorMessage = "Postal code must be provided")]
        [DataType(DataType.PostalCode)]
        [StringLength(5, ErrorMessage = "Postal code must be {1} characters long.", MinimumLength = 5)]
        public string PostalCode { get; set; }

        public string Country { get; set; }

        [DataType(DataType.PhoneNumber)]
        //[RegularExpression("^[0-9\\-\\+]{9,15}$")]
        [MinLength(10, ErrorMessage = "Phone must be at least {1} characters long.")]        
        public string Phone { get; set; }
    }
}

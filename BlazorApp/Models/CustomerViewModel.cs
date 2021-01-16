using System.ComponentModel.DataAnnotations;

namespace BlazorApp
{
    public class CustomerViewModel
    {
        public string Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}

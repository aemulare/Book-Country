using System.ComponentModel.DataAnnotations;

namespace BookCountry.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string FullStreetAddress => AddressLine1 + ", " + AddressLine2;

        [Required(ErrorMessage = "required")]
        public string City { get; set; }

        [Required(ErrorMessage = "required")]
        public string State { get; set; }

        [Required(ErrorMessage = "required")]
        public string Zip { get; set; }
    }
}

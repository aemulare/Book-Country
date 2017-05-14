using System.ComponentModel.DataAnnotations;

namespace BookCountry.Models
{
    /// <summary>
    /// Address business model.
    /// </summary>
    public sealed class Address : PersistentEntity
    {
        /// <summary>
        /// Address line 1.
        /// </summary>
        [Required(ErrorMessage="required")]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Address line 2.
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Full street address.
        /// </summary>
        public string FullStreetAddress => AddressLine1 + ", " + AddressLine2;

        /// <summary>
        /// City.
        /// </summary>
        [Required(ErrorMessage="required")]
        public string City { get; set; }

        /// <summary>
        /// US state code.
        /// </summary>
        [Required(ErrorMessage="required")]
        [MaxLength(2)]
        public string State { get; set; }

        /// <summary>
        /// ZIP code.
        /// </summary>
        [Required(ErrorMessage="required")]
        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }
    }
}

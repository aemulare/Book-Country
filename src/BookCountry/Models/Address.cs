namespace BookCountry.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string FullStreetAddress => AddressLine1 + ", " + AddressLine2;
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}

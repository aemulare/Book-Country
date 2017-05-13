namespace BookCountry.Models
{
    public class BookAuthor : PersistentEntity
    {
        public int BookId { get; set; }
        public Author Author { get; set; }

        public int AuthorOrdinal { get; set; }
        public string Role { get; set; }
    }
}

namespace BookCountry.Models
{
    /// <summary>
    /// A base abstract class for all business domain models.
    /// </summary>
    public class PersistentEntity
    {
        /// <summary>
        /// Persistent entity ID.
        /// </summary>
        public int Id { get; set; }
    }
}

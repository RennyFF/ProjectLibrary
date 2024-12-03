namespace ProjectLibrary.Utils.Types
{
    public class Book
    {
        public int Id { get; set; }
        public AuthorCard Author { get; set; }
        public Genre Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PagesCout { get; set; }
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public DateTime AddedInDatabase { get; set; }
        public int RatingStars { get; set; }
        public bool IsBestSeller { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsPromo { get; set; }
    }
}

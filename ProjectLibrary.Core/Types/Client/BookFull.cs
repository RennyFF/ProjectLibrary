namespace ProjectLibrary.Core.Types.Client
{
    public class BookFullAuthor
    {
        public int Id { get; set; }
        public string AuthorFullName { get; set; }
    }
    public class BookFullGenre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
    }
    public class BookFull
    {
        public int Id { get; set; }
        public BookFullAuthor Author { get; set; }
        public BookFullGenre Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PagesCout { get; set; }
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public int RatingStars { get; set; }
        public bool IsBestSeller { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsPromo { get; set; }
    }
}

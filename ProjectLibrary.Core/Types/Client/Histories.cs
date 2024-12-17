namespace ProjectLibrary.Core.Types.Client
{
    public class BookHistory
    {
        public int BookId { get; set; }
        public DateTime AddedInHistory { get; set; }
    }
    public class AuthorHistory
    {
        public int AuthorId { get; set; }
        public DateTime AddedInHistory { get; set; }
    }
    public class GenreHistory
    {
        public int GenreId { get; set; }
        public DateTime AddedInHistory { get; set; }
    }
    public class HistoryStruct
    {
        public List<BookHistory> bookHistory { get; set; } = new List<BookHistory>();
        public List<AuthorHistory> authorHistory { get; set; } = new List<AuthorHistory>();
        public List<GenreHistory> genreHistory { get; set; } = new List<GenreHistory>();
    }
}

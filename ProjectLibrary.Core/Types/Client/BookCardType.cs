namespace ProjectLibrary.Core.Types.Client
{
    public class BookCardType
    {
        public string Title { get; set; }
        public string AuthorFullNameShort { get; set; }
        public byte[] Image { get; set; }
        public int RatingStars { get; set; }
        public DateTime AddedInDatabase { get; set; }
        public int Id { get; set; }
    }
}

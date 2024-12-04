namespace ProjectLibrary.Utils.Types
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public int ClickedCountity { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PatronomycName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public DateTime BirthdayDate { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<Genre>? ClickedGenres { get; set; }
        public string? LikedObjects { get; set; }
        public string? LastViewed { get; set; }
    }
}

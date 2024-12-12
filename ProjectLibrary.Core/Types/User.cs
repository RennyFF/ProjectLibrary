using ProjectLibrary.Core.Types.Client;

namespace ProjectLibrary.Core.Types
{
    public class UserType
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
        public List<FavGenreType>? ClickedGenres { get; set; }
    }
}

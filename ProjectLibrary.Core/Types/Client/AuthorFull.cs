namespace ProjectLibrary.Core.Types.Client
{
    public class AuthorFull
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public byte[] ImageAvatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
    }
}

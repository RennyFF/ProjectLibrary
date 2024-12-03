namespace ProjectLibrary.Utils.Types
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public byte[] ImageAvatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
    }
}

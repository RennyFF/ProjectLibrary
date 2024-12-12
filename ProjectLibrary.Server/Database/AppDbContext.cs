using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProjectLibrary.Server.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AuthorSet> Authors { get; set; }
        public DbSet<BookSet> Books { get; set; }
        public DbSet<FavoriteBookSet> FavoriteBooks { get; set; }
        public DbSet<FavoriteGenreSet> FavoriteGenres { get; set; }
        public DbSet<GenreSet> Genres { get; set; }
        public DbSet<OrderSet> Orders { get; set; }
        public DbSet<UserSet> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("ProjectLibrary");
        }
        public class AuthorSet
        {
            public int Id { get; set; }
            public string SecondName { get; set; }
            public string FirstName { get; set; }
            public string PatronomycName { get; set; }
            public byte[] ImageAvatar { get; set; }
            public long? DateOfBirthday { get; set; }
            public long? DateOfDeath { get; set; }
        }
        public class BookSet
        {
            public int Id { get; set; }
            public int AuthorId { get; set; }
            public int GenreId { get; set; }
            public long PublicationDate { get; set; }
            public int PagesCout { get; set; }
            public byte[] Image { get; set; }
            public string Title { get; set; }
            public long AddedInDatabase { get; set; }
            public int RatingStars { get; set; }
            public bool IsBestSeller { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public bool IsPromo { get; set; }
        }
        [PrimaryKey(nameof(UserId), nameof(BookId))]
        public class FavoriteBookSet
        {
            public int UserId { get; set; }
            public int BookId { get; set; }
        }
        [PrimaryKey(nameof(UserId), nameof(GenreId))]
        public class FavoriteGenreSet
        {
            public int UserId { get; set; }
            public int GenreId { get; set; }
            public int ClickedCountity { get; set; }
        }
        public class GenreSet
        {
            public int Id { get; set; }
            public string GenreName { get; set; }
            public byte[] ImageAvatar { get; set; }
        }
        public class OrderSet
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int BookId { get; set; }
            public long OrderDate { get; set; }
            public bool IsFromPromo { get; set; }
        }
        public class UserSet
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string PatronomycName { get; set; }
            public string Email { get; set; }
            public string Login { get; set; }
            public string PasswordHash { get; set; }
            public long BirthdayDate { get; set; }
            public long DateOfCreation { get; set; }
            public long LastUpdated { get; set; }
        }
    } 
}

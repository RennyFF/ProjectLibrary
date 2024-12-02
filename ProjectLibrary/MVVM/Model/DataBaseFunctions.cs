using Npgsql;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Converters;
using ProjectLibrary.Utils.Types;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Reflection.Metadata.BlobBuilder;

namespace ProjectLibrary.MVVM.Model
{
    public static class DataBaseFunctions
    {
        public async static Task<User?> GetCurrentUser(NpgsqlConnection Connection, string Login, string PasswordHash)
        {
            List<Genre> AllGenres = await GetAllGenres(Connection);
            string Query = $"SELECT * FROM \"MasterProjectLibrary\".\"Users\"\r\nWhere \"Users\".\"Login\" = '{Login}' and \"Users\".\"PasswordHash\" = '{PasswordHash}' ";
            using var Command = new NpgsqlCommand(Query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var user = new User
                    {
                        Id = Reader.GetInt32(0),
                        FirstName = Reader.GetString(1),
                        SecondName = Reader.GetString(2),
                        PatronomycName = Reader.GetString(3),
                        Email = Reader.GetString(4),
                        Login = Reader.GetString(5),
                        PasswordHash = Reader.GetString(6),
                        BirthdayDate = Reader.GetDateTime(7),
                        DateOfCreation = UnixTimeConverter.TimeStampToDateTime(Reader.GetInt32(8)),
                        LastUpdated = UnixTimeConverter.TimeStampToDateTime(Reader.GetInt32(9)),
                        ClickedGenres = Utils.AddoptationGenres.FromStringToGenresList(Reader.IsDBNull(10) ? null : Reader.GetString(10), AllGenres),
                        LikedObjects = Reader.IsDBNull(11) ? null : Reader.GetString(11),
                        LastViewed = Reader.IsDBNull(12) ? null : Reader.GetString(12)
                    };
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async static Task<List<Genre>> GetAllGenres(NpgsqlConnection Connection)
        {
            string query = $"SELECT * FROM \"MasterProjectLibrary\".\"Genres\"";
            List<Genre> AllGenres = new List<Genre>();
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var genre = new Genre
                    {
                        Id = Reader.GetInt32(0),
                        GenreName = Reader.GetString(1)
                    };
                    AllGenres.Add(genre);
                }
                return AllGenres;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async static Task<ObservableCollection<AuthorCard>> GetAllAuthorsCards(NpgsqlConnection Connection)
        {
            try
            {
                ObservableCollection<AuthorCard> Authors = new ObservableCollection<AuthorCard>();
                string query = $"SELECT author.\"SecondName\", author.\"FirstName\", author.\"PatronomycName\",author.\"ImageAvatar\", author.\"Id\"  " +
                    $"FROM \"MasterProjectLibrary\".\"Authors\" author";
                using var Command = new NpgsqlCommand(query, Connection);
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var book = new AuthorCard
                    {
                        FullName = $"{Reader.GetString(0)} {Reader.GetString(1)[0]}. {Reader.GetString(2)[0]}.",
                        ImageAvatar = (byte[])Reader["ImageAvatar"],
                        Id = Reader.GetInt32(4)
                    };
                    Authors.Add(book);
                }
                return Authors;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async static Task<ObservableCollection<GenreCard>> GetAllGenresCards(NpgsqlConnection Connection)
        {
            string query = $"SELECT genre.\"GenreName\",genre.\"ImageAvatar\", genre.\"Id\" FROM \"MasterProjectLibrary\".\"Genres\" genre";
            ObservableCollection<GenreCard> AllGenres = new ObservableCollection<GenreCard>();
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var genre = new GenreCard
                    {
                        GenreName = Reader.GetString(0),
                        ImageAvatar = (byte[])Reader["ImageAvatar"],
                        Id = Reader.GetInt32(2)
                    };
                    AllGenres.Add(genre);
                }
                return AllGenres;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async void AddUser(NpgsqlConnection Connection, User NewUser)
        {
            string Query = "INSERT INTO \"MasterProjectLibrary\".\"Users\" " +
                "(\"FirstName\", \"SecondName\", \"PatronomycName\", \"Email\", \"Login\", \"PasswordHash\", \"BirthdayDate\", \"DateOfCreation\", \"LastUpdated\") " +
                "VALUES (@Firstname, @SecondName, @PatronomycName, @Email, @Login, @PasswordHash, @BirthdayDate, @DateOfCreation, @LastUpdated)";
            using var command = new NpgsqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@FirstName", NewUser.FirstName);
            command.Parameters.AddWithValue("@SecondName", NewUser.SecondName);
            command.Parameters.AddWithValue("@PatronomycName", NewUser.PatronomycName);
            command.Parameters.AddWithValue("@Email", NewUser.Email);
            command.Parameters.AddWithValue("@Login", NewUser.Login);
            command.Parameters.AddWithValue("@BirthdayDate", NewUser.BirthdayDate);
            command.Parameters.AddWithValue("@PasswordHash", PasswordConverters.FromPasswordToHash(NewUser.PasswordHash));
            command.Parameters.AddWithValue("@DateOfCreation", UnixTimeConverter.DateTimeToTimeStamp(NewUser.DateOfCreation));
            command.Parameters.AddWithValue("@LastUpdated", UnixTimeConverter.DateTimeToTimeStamp(NewUser.LastUpdated));
            await command.ExecuteNonQueryAsync();
        }
        public static async void AddBook(NpgsqlConnection Connection, Book NewBook)
        {
            string Query = "INSERT INTO \"MasterProjectLibrary\".\"Books\" " +
                "(\"AuthorId\", \"GenreId\", \"PublicationDate\", \"PagesCount\", \"Image\", \"AddedInDatabase\") " +
                "VALUES (@AuthorId, @GenreId, @PublicationDate, @PagesCount, @Image, @AddedInDatabase)";
            using var command = new NpgsqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@AuthorId", NewBook.Author.Id);
            command.Parameters.AddWithValue("@GenreId", 1);
            command.Parameters.AddWithValue("@PublicationDate", NewBook.PublicationDate);
            command.Parameters.AddWithValue("@PagesCount", NewBook.PagesCout);
            command.Parameters.AddWithValue("@Image", File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\Resources\\photo.png"));
            command.Parameters.AddWithValue("@Image", File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\Resources\\photo.png"));
            await command.ExecuteNonQueryAsync();
        }

        public static async void UpdateBook(NpgsqlConnection Connection)
        {
            string Query = "UPDATE \"MasterProjectLibrary\".\"Books\" SET " +
                $"\"Image\" = @Image " +
                "WHERE \"Books\".\"Id\" = 3";
            using var command = new NpgsqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@Image", File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\Resources\\photo.png"));
            await command.ExecuteNonQueryAsync();
        }

        public async static Task<bool> CheckIfUnique(NpgsqlConnection Connection, bool IsLogin, string CheckingString)
        {
            string Query;
            if (IsLogin)
            {
                Query = $"SELECT * FROM \"MasterProjectLibrary\".\"Users\"\r\nWhere \"Users\".\"Login\" = '{CheckingString}'";
            }
            else
            {
                Query = $"SELECT * FROM \"MasterProjectLibrary\".\"Users\"\r\nWhere \"Users\".\"Email\" = '{CheckingString}'";
            }
            using var Command = new NpgsqlCommand(Query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public async static Task<bool> CheckIfOwned(NpgsqlConnection Connection, int UserId, int BookId)
        {
            string Query = $"SELECT * FROM \"MasterProjectLibrary\".\"Orders\" WHERE \"UserId\"={UserId} and \"BookId\"={BookId}";
            using var Command = new NpgsqlCommand(Query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                return Reader.HasRows;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async static Task<bool> CheckIfFavorite(NpgsqlConnection Connection, int UserId, int BookId)
        {
            string Query = $"SELECT * FROM \"MasterProjectLibrary\".\"Favorites\" WHERE \"UserId\"={UserId} and \"BookId\"={BookId}";
            using var Command = new NpgsqlCommand(Query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                return Reader.HasRows;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async void ChangeFavoriteBook(NpgsqlConnection Connection, int UserId, int BookId, bool Status)
        {
            string Query;
            if (Status) { 
            Query = "INSERT INTO \"MasterProjectLibrary\".\"Favorites\" " +
                "(\"UserId\", \"BookId\") " +
                "VALUES (@UserId, @BookId)";
            using var command = new NpgsqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@UserId", UserId);
            command.Parameters.AddWithValue("@BookId", BookId);
            await command.ExecuteNonQueryAsync();
            }
            else
            {
                Query = $"DELETE FROM \"MasterProjectLibrary\".\"Favorites\" fav WHERE fav.\"BookId\" = {BookId} and fav.\"UserId\" = {UserId};";
                using var command = new NpgsqlCommand(Query, Connection);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async static Task<List<Book>> GetAllBooks(NpgsqlConnection Connection)
        {
            string query = $"SELECT book.\"Id\", author.\"Id\", author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"genre.\"GenreName\", book.\"PublicationDate\", book.\"PagesCount\", book.\"Image\", book.\"Title\", book.\"AddedInDatabase\", book.\"RatingStars\" " +
                $"FROM \"MasterProjectLibrary\".\"Books\" book join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\"";
            List<Book> AllBooks = new List<Book>();
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var book = new Book
                    {
                        Id = Reader.GetInt32(0),
                        Author = new AuthorCard() { Id = Reader.GetInt32(1), FullName = $"{Reader.GetString(2)} {Reader.GetString(3)} {Reader.GetString(4)}" },
                        Genre = Reader.GetString(5),
                        PublicationDate = Reader.GetDateTime(6),
                        PagesCout = Reader.GetInt32(7),
                        Image = (byte[])Reader["Image"],
                        Title = Reader.GetString(9),
                        AddedInDatabase = Reader.GetDateTime(10),
                        RatingStars = Reader.GetInt32(11)
                    };
                    AllBooks.Add(book);
                }
                return AllBooks;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async static Task<Book> GetFullBook(NpgsqlConnection Connection, int BookId)
        {
            string query = $"SELECT book.\"Id\", author.\"Id\", author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"genre.\"GenreName\", book.\"PublicationDate\", book.\"PagesCount\", book.\"Image\", book.\"Title\", book.\"AddedInDatabase\", book.\"RatingStars\"," +
                $"book.\"IsBestSeller\", book.\"Description\", book.\"Price\", book.\"IsPromo\"" +
                $"FROM \"MasterProjectLibrary\".\"Books\" book join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" Where book.\"Id\" = {BookId}";
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var book = new Book
                    {
                        Id = Reader.GetInt32(0),
                        Author = new AuthorCard() { Id = Reader.GetInt32(1), FullName = $"{Reader.GetString(2)} {Reader.GetString(3)} {Reader.GetString(4)}" },
                        Genre = Reader.GetString(5),
                        PublicationDate = Reader.GetDateTime(6),
                        PagesCout = Reader.GetInt32(7),
                        Image = (byte[])Reader["Image"],
                        Title = Reader.GetString(9),
                        AddedInDatabase = Reader.GetDateTime(10),
                        RatingStars = Reader.GetInt32(11),
                        IsBestSeller = Reader.GetBoolean(12),
                        Description = Reader.GetString(13),
                        Price = Reader.GetDecimal(14),
                        IsPromo = Reader.GetBoolean(15),
                    };
                    return book;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async static Task<ObservableCollection<BookCard>> GetNewBooks(NpgsqlConnection Connection)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\"" +
                $" FROM \"MasterProjectLibrary\".\"Books\" book join \"MasterProjectLibrary\".\"Authors\"" +
                $" author on book.\"AuthorId\" = author.\"Id\" join " +
                $"\"MasterProjectLibrary\".\"Genres\" \r\ngenre on genre.\"Id\" = book.\"GenreId\" " +
                $"Where book.\"RatingStars\" between 4 and 5 Order by book.\"AddedInDatabase\" desc limit 12;";
            ObservableCollection<BookCard> Books = new ObservableCollection<BookCard>();
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var book = new BookCard
                    {
                        Author = $"{Reader.GetString(0)} {Reader.GetString(1)[0]}. {Reader.GetString(2)[0]}.",
                        ImageSource = (byte[])Reader["Image"],
                        Title = Reader.GetString(4),
                        RatingStars = Reader.GetInt32(5),
                        AddedInDatabase = Reader.GetDateTime(6),
                        BookId = Reader.GetInt32(7),
                        AuthorId = Reader.GetInt32(8),
                        GenreId = Reader.GetInt32(9)
                    };
                    Books.Add(book);
                }
                return Books;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async static Task<int> GetMagicBook(NpgsqlConnection Connection)
        {
            string query = $"SELECT book.\"Id\"" +
                $" FROM \"MasterProjectLibrary\".\"Books\" book" +
                $"Where book.\"IsPromo\" = true;";
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    return Reader.GetInt32(0);
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
        public async static Task<ObservableCollection<BookCard>> GetBestSellers(NpgsqlConnection Connection)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\"" +
                $" FROM \"MasterProjectLibrary\".\"Books\" book join \"MasterProjectLibrary\".\"Authors\"" +
                $" author on book.\"AuthorId\" = author.\"Id\" join " +
                $"\"MasterProjectLibrary\".\"Genres\" \r\ngenre on genre.\"Id\" = book.\"GenreId\" " +
                $"Where book.\"IsBestSeller\" = true limit 15;";
            ObservableCollection<BookCard> Books = new ObservableCollection<BookCard>();
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var book = new BookCard
                    {
                        Author = $"{Reader.GetString(0)} {Reader.GetString(1)[0]}. {Reader.GetString(2)[0]}.",
                        ImageSource = (byte[])Reader["Image"],
                        Title = Reader.GetString(4),
                        RatingStars = Reader.GetInt32(5),
                        AddedInDatabase = Reader.GetDateTime(6),
                        BookId = Reader.GetInt32(7),
                        AuthorId = Reader.GetInt32(8),
                        GenreId = Reader.GetInt32(9)
                    };
                    Books.Add(book);
                }
                return Books;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async static Task<int> GetBooksCountity(NpgsqlConnection Connection)
        {
            string query = $"SELECT Count(*) FROM \"MasterProjectLibrary\".\"Books\"";
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    return Convert.ToInt32(Math.Ceiling(Reader.GetInt32(0) / 44.0));
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async static Task<int> GetBooksCountity(NpgsqlConnection Connection, Genre Genre)
        {
            string query = $"SELECT Count(*) FROM \"MasterProjectLibrary\".\"Books\" books Where books.\"GenreId\" = {Genre.Id}";
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    return Convert.ToInt32(Math.Ceiling(Reader.GetInt32(0) / 44.0));
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async static Task<ObservableCollection<BookCard>> GetBooksByPage(NpgsqlConnection Connection, int Page, int CountityOnPage)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\"" +
                $"FROM \"MasterProjectLibrary\".\"Books\" book join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" limit {CountityOnPage} offset {Page * CountityOnPage}";
            ObservableCollection<BookCard> Books = new ObservableCollection<BookCard>();
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var book = new BookCard
                    {
                        Author = $"{Reader.GetString(0)} {Reader.GetString(1)[0]}. {Reader.GetString(2)[0]}.",
                        ImageSource = (byte[])Reader["Image"],
                        Title = Reader.GetString(4),
                        RatingStars = Reader.GetInt32(5),
                        AddedInDatabase = Reader.GetDateTime(6),
                        BookId = Reader.GetInt32(7),
                        AuthorId = Reader.GetInt32(8),
                        GenreId = Reader.GetInt32(9)
                    };
                    Books.Add(book);
                }
                return Books;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async static Task<ObservableCollection<BookCard>> GetBooksByPageGenre(NpgsqlConnection Connection, int Page, int CountityOnPage, Genre Genre)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\"" +
                $"FROM \"MasterProjectLibrary\".\"Books\" book join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" Where book.\"GenreId\" = {Genre.Id} limit {CountityOnPage} offset {Page * CountityOnPage}";
            ObservableCollection<BookCard> Books = new ObservableCollection<BookCard>();
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var book = new BookCard
                    {
                        Author = $"{Reader.GetString(0)} {Reader.GetString(1)[0]}. {Reader.GetString(2)[0]}.",
                        ImageSource = (byte[])Reader["Image"],
                        Title = Reader.GetString(4),
                        RatingStars = Reader.GetInt32(5),
                        AddedInDatabase = Reader.GetDateTime(6),
                        BookId = Reader.GetInt32(7),
                        AuthorId = Reader.GetInt32(8),
                        GenreId = Reader.GetInt32(9)
                    };
                    Books.Add(book);
                }
                return Books;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async static Task<ObservableCollection<BookCard>> GetBooksFromHistory(NpgsqlConnection Connection, List<BookHistory> HistoryList)
        {
            try
            {
                ObservableCollection<BookCard> Books = new ObservableCollection<BookCard>();
                string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                    $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\"" +
                    $"FROM \"MasterProjectLibrary\".\"Books\" book join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                    $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" Where book.\"Id\" = ";
                foreach (var item in HistoryList)
                {
                    string Finalquery = query + $"{item.BookId}";
                    using var Command = new NpgsqlCommand(Finalquery, Connection);
                    using var Reader = Command.ExecuteReader();
                    while (await Reader.ReadAsync())
                    {
                        var book = new BookCard
                        {
                            Author = $"{Reader.GetString(0)} {Reader.GetString(1)[0]}. {Reader.GetString(2)[0]}.",
                            ImageSource = (byte[])Reader["Image"],
                            Title = Reader.GetString(4),
                            RatingStars = Reader.GetInt32(5),
                            AddedInDatabase = Reader.GetDateTime(6),
                            BookId = Reader.GetInt32(7),
                            AuthorId = Reader.GetInt32(8),
                            GenreId = Reader.GetInt32(9)
                        };
                        Books.Add(book);
                    }
                }
                return Books;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async static Task<ObservableCollection<AuthorCard>> GetAuthorsFromHistory(NpgsqlConnection Connection, List<AuthorHistory> HistoryList)
        {
            try
            {
                ObservableCollection<AuthorCard> Authors = new ObservableCollection<AuthorCard>();
                string query = $"SELECT author.\"SecondName\", author.\"FirstName\", author.\"PatronomycName\",author.\"ImageAvatar\", author.\"Id\"  " +
                    $"FROM \"MasterProjectLibrary\".\"Authors\" author Where author.\"Id\" = ";
                foreach (var item in HistoryList)
                {
                    string Finalquery = query + $"{item.AuthorId}";
                    using var Command = new NpgsqlCommand(Finalquery, Connection);
                    using var Reader = Command.ExecuteReader();
                    while (await Reader.ReadAsync())
                    {
                        var book = new AuthorCard
                        {
                            FullName = $"{Reader.GetString(0)} {Reader.GetString(1)[0]}. {Reader.GetString(2)[0]}.",
                            ImageAvatar = (byte[])Reader["ImageAvatar"],
                            Id = Reader.GetInt32(4)
                        };
                        Authors.Add(book);
                    }
                }
                return Authors;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async static Task<ObservableCollection<GenreCard>> GetGenreFromHistory(NpgsqlConnection Connection, List<GenreHistory> HistoryList)
        {
            try
            {
                ObservableCollection<GenreCard> Genres = new ObservableCollection<GenreCard>();
                string query = $"SELECT genre.\"GenreName\",genre.\"ImageAvatar\", genre.\"Id\"  " +
                    $"FROM \"MasterProjectLibrary\".\"Genres\" genre Where genre.\"Id\" = ";
                foreach (var item in HistoryList)
                {
                    string Finalquery = query + $"{item.GenreId}";
                    using var Command = new NpgsqlCommand(Finalquery, Connection);
                    using var Reader = Command.ExecuteReader();
                    while (await Reader.ReadAsync())
                    {
                        var genre = new GenreCard
                        {
                            GenreName = Reader.GetString(0),
                            ImageAvatar = (byte[])Reader["ImageAvatar"],
                            Id = Reader.GetInt32(2)
                        };
                        Genres.Add(genre);
                    }
                }
                return Genres;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

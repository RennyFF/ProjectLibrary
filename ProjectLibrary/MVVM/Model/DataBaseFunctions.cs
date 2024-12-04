using Npgsql;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Converters;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Windows.Controls.Primitives;

namespace ProjectLibrary.MVVM.Model
{
    public static class DataBaseFunctions
    {
        #region Utils
        private static async Task<List<Genre>?> GetClickedHistory(NpgsqlConnection Connection, int UserId)
        {
            List<Genre> Result = new();
            string Query = $"SELECT genre.\"Id\", genre.\"GenreName\", favGenre.\"ClickedCountity\" " +
                $"FROM \"MasterProjectLibrary\".\"FavoriteGenres\" favGenre " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = favGenre.\"GenreId\" " +
                $"Where favGenre.\"UserId\" = {UserId}";
            using var Command = new NpgsqlCommand(Query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var genre = new Genre
                    {
                        Id = Reader.GetInt32(0),
                        GenreName = Reader.GetString(1),
                        ClickedCountity = Reader.GetInt32(2),
                    };
                    Result.Add(genre);
                }
                return Result.Count > 0 ? Result : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region User tasks
        public async static Task<User?> GetCurrentUser(NpgsqlConnection Connection, string Login, string PasswordHash)
        {
            User ResultUser = new User();
            string Query = $"SELECT * FROM \"MasterProjectLibrary\".\"Users\"\r\nWhere \"Users\".\"Login\" = '{Login}' and \"Users\".\"PasswordHash\" = '{PasswordHash}' ";
            using var Command = new NpgsqlCommand(Query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    ResultUser.Id = Reader.GetInt32(0);
                    ResultUser.FirstName = Reader.GetString(1);
                    ResultUser.SecondName = Reader.GetString(2);
                    ResultUser.PatronomycName = Reader.GetString(3);
                    ResultUser.Email = Reader.GetString(4);
                    ResultUser.Login = Reader.GetString(5);
                    ResultUser.PasswordHash = Reader.GetString(6);
                    ResultUser.BirthdayDate = Reader.GetDateTime(7);
                    ResultUser.DateOfCreation = UnixTimeConverter.TimeStampToDateTime(Reader.GetInt32(8));
                    ResultUser.LastUpdated = UnixTimeConverter.TimeStampToDateTime(Reader.GetInt32(9));
                    ResultUser.LikedObjects = Reader.IsDBNull(10) ? null : Reader.GetString(10);
                    ResultUser.LastViewed = Reader.IsDBNull(11) ? null : Reader.GetString(11);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            Command.Dispose();
            ResultUser.ClickedGenres = await GetClickedHistory(Connection, ResultUser.Id);
            return ResultUser;
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
        public async static Task<bool> CheckIfUniqueUser(NpgsqlConnection Connection, bool IsLogin, string CheckingString)
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
        #endregion
        #region Genre tasks
        public async static Task<int> GetGenresCountity(NpgsqlConnection Connection)
        {
            string query = $"SELECT Count(*) FROM \"MasterProjectLibrary\".\"Genres\"";
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    return Convert.ToInt32(Math.Ceiling(Reader.GetInt32(0) / 27.0));
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async static Task<ObservableCollection<GenreCard>> GetGenresByPage(NpgsqlConnection Connection, int Page, int CountityOnPage)
        {
            string query = $"SELECT genre.\"GenreName\",genre.\"ImageAvatar\", genre.\"Id\" " +
                    $"FROM \"MasterProjectLibrary\".\"Genres\" genre " +
                $"limit {CountityOnPage} offset {Page * CountityOnPage}";
            ObservableCollection<GenreCard> Genres = new ObservableCollection<GenreCard>();
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
                    Genres.Add(genre);
                }
                return Genres;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async static Task<ObservableCollection<GenreCard>> GetAllGenreCards(NpgsqlConnection Connection)
        {
            string query = $"SELECT genre.\"GenreName\",genre.\"ImageAvatar\",genre.\"Id\" " +
                $"FROM \"MasterProjectLibrary\".\"Genres\" genre";
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
        public async static Task<ObservableCollection<GenreCard>> GetAllGenreCards(NpgsqlConnection Connection, List<GenreHistory> HistoryList)
        {
            try
            {
                ObservableCollection<GenreCard> Genres = new ObservableCollection<GenreCard>();
                string query = $"SELECT genre.\"GenreName\",genre.\"ImageAvatar\", genre.\"Id\"  " +
                    $"FROM \"MasterProjectLibrary\".\"Genres\" genre " +
                    $"Where genre.\"Id\" = ";
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
        public async static Task<GenreCard> GetSingleGenreCard(NpgsqlConnection Connection, int GenreId)
        {
            try
            {
                string query = $"SELECT genre.\"Id\", genre.\"GenreName\", genre.\"ImageAvatar\" " +
                    $"FROM \"MasterProjectLibrary\".\"Genres\" genre " +
                    $"Where genre.\"Id\"= {GenreId}";
                using var Command = new NpgsqlCommand(query, Connection);
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var genre = new GenreCard
                    {
                        Id = Reader.GetInt32(0),
                        GenreName = Reader.GetString(1),
                        ImageAvatar = (byte[])Reader["ImageAvatar"],
                    };
                    return genre;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Author tasks
        public async static Task<int> GetAuthorsCountity(NpgsqlConnection Connection)
        {
            string query = $"SELECT Count(*) FROM \"MasterProjectLibrary\".\"Authors\"";
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    return Convert.ToInt32(Math.Ceiling(Reader.GetInt32(0) / 27.0));
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async static Task<ObservableCollection<AuthorCard>> GetAuthorsByPage(NpgsqlConnection Connection, int Page, int CountityOnPage)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\", author.\"PatronomycName\",author.\"ImageAvatar\", author.\"Id\" " +
                $"FROM \"MasterProjectLibrary\".\"Authors\" author " +
                $"limit {CountityOnPage} offset {Page * CountityOnPage}";
            ObservableCollection<AuthorCard> Authors = new ObservableCollection<AuthorCard>();
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var author = new AuthorCard
                    {
                        FullName = $"{Reader.GetString(0)} {Reader.GetString(1)[0]}. {Reader.GetString(2)[0]}.",
                        ImageAvatar = (byte[])Reader["ImageAvatar"],
                        Id = Reader.GetInt32(4)
                    };
                    Authors.Add(author);
                }
                return Authors;
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
                string query = $"SELECT author.\"SecondName\", author.\"FirstName\", author.\"PatronomycName\",author.\"ImageAvatar\", author.\"Id\" " +
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
        public async static Task<ObservableCollection<AuthorCard>> GetAllAuthorsCards(NpgsqlConnection Connection, List<AuthorHistory> HistoryList)
        {
            try
            {
                ObservableCollection<AuthorCard> Authors = new ObservableCollection<AuthorCard>();
                string query = $"SELECT author.\"SecondName\", author.\"FirstName\", author.\"PatronomycName\",author.\"ImageAvatar\", author.\"Id\" " +
                    $"FROM \"MasterProjectLibrary\".\"Authors\" author " +
                    $"Where author.\"Id\" = ";
                foreach (var item in HistoryList)
                {
                    string Finalquery = query + $"{item.AuthorId}";
                    using var Command = new NpgsqlCommand(Finalquery, Connection);
                    using var Reader = Command.ExecuteReader();
                    while (await Reader.ReadAsync())
                    {
                        var author = new AuthorCard
                        {
                            FullName = $"{Reader.GetString(0)} {Reader.GetString(1)[0]}. {Reader.GetString(2)[0]}.",
                            ImageAvatar = (byte[])Reader["ImageAvatar"],
                            Id = Reader.GetInt32(4)
                        };
                        Authors.Add(author);
                    }
                }
                return Authors;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async static Task<Author> GetSingleAuthorCard(NpgsqlConnection Connection, int AuthorId)
        {
            try
            {
                string query = $"SELECT author.\"SecondName\", author.\"FirstName\", author.\"PatronomycName\",author.\"ImageAvatar\", author.\"Id\", author.\"DateOfBirthday\", author.\"DateOfDeath\" " +
                    $"FROM \"MasterProjectLibrary\".\"Authors\" author " +
                    $"Where author.\"Id\"= {AuthorId}";
                using var Command = new NpgsqlCommand(query, Connection);
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    var author = new Author
                    {
                        FullName = $"{Reader.GetString(0)} {Reader.GetString(1)} {Reader.GetString(2)}",
                        ImageAvatar = (byte[])Reader["ImageAvatar"],
                        Id = Reader.GetInt32(4),
                        DateOfBirth = Reader.IsDBNull(5) ? null : Reader.GetDateTime(5),
                        DateOfDeath = Reader.IsDBNull(6) ? null : Reader.GetDateTime(6)
                    };
                    return author;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Book tasks
        public async static Task<bool> CheckIfOwned(NpgsqlConnection Connection, int UserId, int BookId)
        {
            string Query = $"SELECT * FROM \"MasterProjectLibrary\".\"Orders\" " +
                $"WHERE \"UserId\"={UserId} and \"BookId\"={BookId}";
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
            string Query = $"SELECT * FROM \"MasterProjectLibrary\".\"FavoriteBooks\" " +
                $"WHERE \"UserId\"={UserId} and \"BookId\"={BookId}";
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
            if (Status)
            {
                Query = "INSERT INTO \"MasterProjectLibrary\".\"FavoriteBooks\" " +
                    "(\"UserId\", \"BookId\") " +
                    "VALUES (@UserId, @BookId)";
                using var command = new NpgsqlCommand(Query, Connection);
                command.Parameters.AddWithValue("@UserId", UserId);
                command.Parameters.AddWithValue("@BookId", BookId);
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                Query = $"DELETE FROM \"MasterProjectLibrary\".\"FavoriteBooks\" fav WHERE fav.\"BookId\" = {BookId} and fav.\"UserId\" = {UserId};";
                using var command = new NpgsqlCommand(Query, Connection);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async static Task<Book> GetSingleBookCard(NpgsqlConnection Connection, int BookId)
        {
            string query = $"SELECT book.\"Id\", author.\"Id\", author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", genre.\"Id\", " +
                $"genre.\"GenreName\", book.\"PublicationDate\", book.\"PagesCount\", book.\"Image\", book.\"Title\", book.\"AddedInDatabase\", book.\"RatingStars\"," +
                $"book.\"IsBestSeller\", book.\"Description\", book.\"Price\", book.\"IsPromo\"" +
                $"FROM \"MasterProjectLibrary\".\"Books\" book " +
                $"join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" " +
                $"Where book.\"Id\" = {BookId}";
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
                        Genre = new Genre() { Id = Reader.GetInt32(5), GenreName = Reader.GetString(6) },
                        PublicationDate = Reader.GetDateTime(7),
                        PagesCout = Reader.GetInt32(8),
                        Image = (byte[])Reader["Image"],
                        Title = Reader.GetString(10),
                        AddedInDatabase = Reader.GetDateTime(11),
                        RatingStars = Reader.GetInt32(12),
                        IsBestSeller = Reader.GetBoolean(13),
                        Description = Reader.GetString(14),
                        Price = Reader.GetDecimal(15),
                        IsPromo = Reader.GetBoolean(16),
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
        public async static Task<ObservableCollection<BookCard>> GetBestSellersBookCards(NpgsqlConnection Connection)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\" " +
                $"FROM \"MasterProjectLibrary\".\"Books\" book " +
                $"join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" " +
                $"Where book.\"IsBestSeller\" = true " +
                $"limit 15;";
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
                        Id = Reader.GetInt32(7),
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
        public async static Task<ObservableCollection<BookCard>> GetNewBookCards(NpgsqlConnection Connection)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\" " +
                $"FROM \"MasterProjectLibrary\".\"Books\" book " +
                $"join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" " +
                $"Where book.\"RatingStars\" between 4 and 5 " +
                $"Order by book.\"AddedInDatabase\" desc " +
                $"limit 12;";
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
                        Id = Reader.GetInt32(7),
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
        public async static Task<int> GetMagicBookId(NpgsqlConnection Connection)
        {
            string query = $"SELECT book.\"Id\" " +
                $"FROM \"MasterProjectLibrary\".\"Books\" book " +
                $"Where book.\"IsPromo\" = true;";
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    return Reader.GetInt32(0);
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
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
                    return Convert.ToInt32(Math.Ceiling(Reader.GetInt32(0) / 27.0));
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async static Task<int> GetBooksCountity(NpgsqlConnection Connection, int UserId)
        {
            string query = $"SELECT Count(*) FROM \"MasterProjectLibrary\".\"FavoriteBooks\" fav Where fav.\"UserId\" = {UserId}";
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    return Convert.ToInt32(Math.Ceiling(Reader.GetInt32(0) / 27.0));
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async static Task<int> GetBooksCountity(NpgsqlConnection Connection, GenreCard Genre)
        {
            string query = $"SELECT Count(*) FROM \"MasterProjectLibrary\".\"Books\" books Where books.\"GenreId\" = {Genre.Id}";
            using var Command = new NpgsqlCommand(query, Connection);
            try
            {
                using var Reader = Command.ExecuteReader();
                while (await Reader.ReadAsync())
                {
                    return Convert.ToInt32(Math.Ceiling(Reader.GetInt32(0) / 27.0));
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async static Task<ObservableCollection<BookCard>> GetBooksByPage(NpgsqlConnection Connection, int Page, int CountityOnPage, GenreCard GenreId)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\"" +
                $"FROM \"MasterProjectLibrary\".\"Books\" book " +
                $"join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" " +
                $"Where book.\"GenreId\" = {GenreId.Id} " +
                $"limit {CountityOnPage} offset {Page * CountityOnPage}";
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
                        Id = Reader.GetInt32(7),
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
        public async static Task<ObservableCollection<BookCard>> GetBooksByPage(NpgsqlConnection Connection, int Page, int CountityOnPage, int UserId)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\"" +
                $"FROM \"MasterProjectLibrary\".\"Books\" book join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" " +
                $"join \"MasterProjectLibrary\".\"FavoriteBooks\" fav on fav.\"BookId\" = book.\"Id\" " +
                $"Where fav.\"UserId\" = {UserId} " +
                $"limit {CountityOnPage} offset {Page * CountityOnPage}";
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
                        Id = Reader.GetInt32(7),
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
        public async static Task<ObservableCollection<BookCard>> GetBooksByPage(NpgsqlConnection Connection, int Page, int CountityOnPage)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\"" +
                $"FROM \"MasterProjectLibrary\".\"Books\" book " +
                $"join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" " +
                $"limit {CountityOnPage} offset {Page * CountityOnPage}";
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
                        Id = Reader.GetInt32(7),
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
        public async static Task<ObservableCollection<BookCard>> GetAllBookCards(NpgsqlConnection Connection, int AuthorId)
        {
            string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\"" +
                $"FROM \"MasterProjectLibrary\".\"Books\" book " +
                $"join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" " +
                $"Where author.\"Id\"={AuthorId}";
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
                        Id = Reader.GetInt32(7),
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
        public async static Task<ObservableCollection<BookCard>> GetAllBookCards(NpgsqlConnection Connection, List<BookHistory> HistoryList)
        {
            try
            {
                ObservableCollection<BookCard> Books = new ObservableCollection<BookCard>();
                string query = $"SELECT author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                    $"book.\"Image\", book.\"Title\", book.\"RatingStars\", book.\"AddedInDatabase\", book.\"Id\", author.\"Id\", genre.\"Id\"" +
                    $"FROM \"MasterProjectLibrary\".\"Books\" book " +
                    $"join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                    $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" " +
                    $"Where book.\"Id\" = ";
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
                            Id = Reader.GetInt32(7),
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
        #endregion
    }
}

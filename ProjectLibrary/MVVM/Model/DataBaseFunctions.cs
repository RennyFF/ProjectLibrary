using Npgsql;
using ProjectLibrary.Utils.Converters;
using ProjectLibrary.Utils.Types;
using System.Data;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectLibrary.MVVM.Model
{
    public static class DataBaseFunctions
    {
        public async static void ExecuteNonQueryDB(string QueryString, NpgsqlConnection Connection)
        {
            using var Command = new NpgsqlCommand(QueryString, Connection);
            await Command.ExecuteNonQueryAsync();
        }
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
            if(IsLogin)
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

        public async static Task<List<Book>> GetAllBooks(NpgsqlConnection Connection)
        {
            string query = $"SELECT book.\"Id\", author.\"Id\", author.\"SecondName\", author.\"FirstName\",author.\"PatronomycName\", " +
                $"genre.\"GenreName\", book.\"PublicationDate\", book.\"PagesCount\", book.\"Image\", book.\"Title\", book.\"AddedInDatabase\" " +
                $"FROM \"MasterProjectLibrary\".\"Books\" book join \"MasterProjectLibrary\".\"Authors\" author on book.\"AuthorId\" = author.\"Id\" " +
                $"join \"MasterProjectLibrary\".\"Genres\" genre on genre.\"Id\" = book.\"GenreId\" " +
                $"Where book.\"Id\" = '3'";
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
                        Author = new Author() { Id = Reader.GetInt32(1), SecondName = Reader.GetString(2), FirstName = Reader.GetString(3), PatronomycName = Reader.GetString(4) },
                        Genre = Reader.GetString(5),
                        PublicationDate = Reader.GetDateTime(6),
                        PagesCout = Reader.GetInt32(7),
                        Image = (byte[])Reader["Image"],
                        Title = Reader.GetString(9),
                        AddedInDatabase = Reader.GetDateTime(10)
                    };
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                    AllBooks.Add(book);
                }
                return AllBooks;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

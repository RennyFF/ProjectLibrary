using Npgsql;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.MVVM.Model
{
    public static class DataBaseFunctions
    {
        public static void ExecuteNonQueryDB(string QueryString, NpgsqlConnection Connection)
        {
            using var Command = new NpgsqlCommand(QueryString, Connection);
            Command.ExecuteNonQuery();
        }
        public static User? GetCurrentUser(NpgsqlConnection Connection, string Login, string PasswordHash)
        {
            var users = new User();

            string query = $"SELECT * FROM \"MasterProjectLibrary\".\"Users\"\r\nWhere \"Users\".\"Login\" = '{Login}' and \"Users\".\"PasswordHash\" = '{PasswordHash}' ";

            using var command = new NpgsqlCommand(query, Connection);
            try {
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        SecondName = reader.GetString(2),
                        PatronomycName = reader.GetString(3),
                        Email = reader.GetString(4),
                        Login = reader.GetString(5),
                        PasswordHash = reader.GetString(6),
                        BirthdayDate = reader.GetDateTime(7),
                        AuthorizationToken = reader.IsDBNull(8) ? null : reader.GetString(8),
                        DateOfCreation = Utils.UnixTimeConverter.TimeStampToDateTime(reader.GetInt32(9)),
                        LastUpdated = Utils.UnixTimeConverter.TimeStampToDateTime(reader.GetInt32(10)),
                        FavoriteGenres = reader.IsDBNull(11) ? null : reader.GetString(11),
                        LikedObjects = reader.IsDBNull(12) ? null : reader.GetString(12),
                        LastViewed = reader.IsDBNull(13) ? null : reader.GetString(13)
                    };
                    users = user;
                }
                return users;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public static void AddUser(NpgsqlConnection Connection, User NewUser)
        {
            string query = @"INSERT INTO Users 
                             (FirstName, SecondName, PatronomycName, Email, Login, PasswordHash, BirthdayDate, 
                              AuthorizationToken, DateOfCreation, LastUpdated, FavoriteGenres, LikedObjects, LastViewed) 
                             VALUES 
                             (@FirstName, @SecondName, @PatronomycName, @Email, @Login, @PasswordHash, @BirthdayDate, 
                              @AuthorizationToken, @DateOfCreation, @LastUpdated, @FavoriteGenres, @LikedObjects, @LastViewed)";

            using var command = new NpgsqlCommand(query, Connection);
            command.Parameters.AddWithValue("@FirstName", NewUser.FirstName);
            command.Parameters.AddWithValue("@SecondName", NewUser.SecondName);
            command.Parameters.AddWithValue("@PatronomycName", NewUser.PatronomycName);
            command.Parameters.AddWithValue("@Email", NewUser.Email);
            command.Parameters.AddWithValue("@Login", NewUser.Login);
            command.Parameters.AddWithValue("@PasswordHash", NewUser.PasswordHash);
            command.Parameters.AddWithValue("@BirthdayDate", NewUser.BirthdayDate);
            command.Parameters.AddWithValue("@AuthorizationToken", NewUser.AuthorizationToken ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@DateOfCreation", NewUser.DateOfCreation);
            command.Parameters.AddWithValue("@LastUpdated", NewUser.LastUpdated);
            command.Parameters.AddWithValue("@FavoriteGenres", NewUser.FavoriteGenres ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@LikedObjects", NewUser.LikedObjects ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@LastViewed", NewUser.LastViewed ?? (object)DBNull.Value);

            command.ExecuteNonQuery();
        }
    }
}

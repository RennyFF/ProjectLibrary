using Microsoft.EntityFrameworkCore;
using ProjectLibrary.Core.Converters;
using ProjectLibrary.Core.Types.Client;
using static ProjectLibrary.Server.Database.AppDbContext;

namespace ProjectLibrary.Server.Database.Requests
{
    public interface IUserRequests
    {
        Task<bool> CheckIfUniqueLogin(string CheckingString);
        Task<bool> CheckIfUniqueEmail(string CheckingString);
        Task AddUser(UserType user);
        Task<UserSet?> GetSingleUser(string Login, string PasswordHash);
    }
    public class UserRequests : IUserRequests
    {
        private readonly AppDbContext _context;

        public UserRequests(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(UserType user)
        {
            var NewUser = new UserSet
            {
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                PatronomycName = user.PatronomycName,
                Email = user.Email,
                Login = user.Login,
                PasswordHash = user.PasswordHash,
                DateOfCreation = UnixTimeConverter.DateTimeToTimeStamp(DateTime.Now),
                LastUpdated = UnixTimeConverter.DateTimeToTimeStamp(DateTime.Now),
                BirthdayDate = UnixTimeConverter.DateTimeToTimeStamp(user.BirthdayDate),
            };
            await _context.Users.AddAsync(NewUser);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfUniqueEmail(string CheckingString)
        {
            return !await _context.Users.AnyAsync(i => i.Email == CheckingString);
        }

        public async Task<bool> CheckIfUniqueLogin(string CheckingString)
        {
            return !await _context.Users.AnyAsync(i => i.Login == CheckingString);
        }
        public async Task<UserSet?> GetSingleUser(string Login, string PasswordHash)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.Login == Login && i.PasswordHash == PasswordHash);
        }
    }
}

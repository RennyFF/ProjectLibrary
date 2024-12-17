using Microsoft.EntityFrameworkCore;
using static ProjectLibrary.Server.Database.AppDbContext;

namespace ProjectLibrary.Server.Database.Requests
{
    public interface IFavoriteGenreRequests
    {
        Task<IEnumerable<FavoriteGenreSet>?> GetFavGenreByUserAsync(int UserId);
    }
    public class FavoriteGenreRequests : IFavoriteGenreRequests
    {
        private readonly AppDbContext _context;

        public FavoriteGenreRequests(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FavoriteGenreSet>?> GetFavGenreByUserAsync(int UserId)
        {
            return await _context.FavoriteGenres.Where(i => i.UserId == UserId).ToListAsync();
        }
    }
}

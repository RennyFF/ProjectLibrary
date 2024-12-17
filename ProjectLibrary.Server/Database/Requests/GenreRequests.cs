using Microsoft.EntityFrameworkCore;
using static ProjectLibrary.Server.Database.AppDbContext;

namespace ProjectLibrary.Server.Database.Requests
{
    public interface IGenreRequests
    {
        Task<int> GetGenreCountityAsync(double CountityOnPage);
        Task<IEnumerable<GenreSet>> GetGenresByPageAsync(int Page, int CountityOnPage);
        Task<IEnumerable<GenreSet>> GetAllGenresAsync();
        Task<GenreSet?> GetSingleGenreAsync(int GenreId);
        Task<string> GetGenreNameByIdAsync(int GenreId);
    }
    public class GenreRequests : IGenreRequests
    {
        private readonly AppDbContext _context;

        public GenreRequests(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenreSet>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<int> GetGenreCountityAsync(double CountityOnPage)
        {
            return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Genres.CountAsync().Result / CountityOnPage)));
        }

        public async Task<string> GetGenreNameByIdAsync(int GenreId)
        {
            return _context.Genres.FirstAsync(i => i.Id == GenreId).Result.GenreName;
        }

        public async Task<IEnumerable<GenreSet>> GetGenresByPageAsync(int Page, int CountityOnPage)
        {
            return await _context.Genres
            .Skip((Page - 1) * CountityOnPage)
            .Take(CountityOnPage)
            .ToListAsync();
        }

        public async Task<GenreSet?> GetSingleGenreAsync(int GenreId)
        {
            return await _context.Genres.FirstOrDefaultAsync(i => i.Id == GenreId);
        }
    }
}

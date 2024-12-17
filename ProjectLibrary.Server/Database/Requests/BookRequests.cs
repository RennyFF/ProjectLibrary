using Microsoft.EntityFrameworkCore;
using static ProjectLibrary.Server.Database.AppDbContext;

namespace ProjectLibrary.Server.Database.Requests
{
    public interface IBookRequests
    {
        Task<int> GetBookCountityAsync(double CountityOnPage);
        Task<int> GetBookCountityByGenreAsync(double CountityOnPage, int GenreId);
        Task<int> GetBookCountityByAuthorAsync(double CountityOnPage, int AuthorId);
        Task<int> GetMagicBookIdAsync();
        Task<IEnumerable<BookSet>> GetBooksByPageAsync(int Page, int CountityOnPage);
        Task<IEnumerable<BookSet>> GetBooksByPageByGenreAsync(int Page, int CountityOnPage, int GenreId);
        Task<IEnumerable<BookSet>> GetBooksByAuthorAsync(int Page, int CountityOnPage, int AuthorId);
        Task<IEnumerable<BookSet>> GetAllBooksAsync();
        Task<BookSet?> GetSingleBookAsync(int BookId);
        Task<IEnumerable<BookSet>> GetBestSellerBooksAsync();
        Task<IEnumerable<BookSet>> GetNewBooksAsync();
    }
    public class BookRequests : IBookRequests
    {
        private readonly AppDbContext _context;

        public BookRequests(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookSet>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<IEnumerable<BookSet>> GetBestSellerBooksAsync()
        {
            return await _context.Books.Where(i => i.IsBestSeller).Take(15).ToListAsync();
        }

        public async Task<int> GetBookCountityAsync(double CountityOnPage)
        {
            return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Books.CountAsync().Result / CountityOnPage)));
        }

        public async Task<int> GetBookCountityByGenreAsync(double CountityOnPage, int GenreId)
        {
            return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Books.Where(i=> i.GenreId == GenreId).CountAsync().Result / CountityOnPage)));
        }
        public async Task<int> GetBookCountityByAuthorAsync(double CountityOnPage, int AuthorId)
        {
            return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Books.Where(i => i.AuthorId == AuthorId).CountAsync().Result / CountityOnPage)));
        }

        public async Task<IEnumerable<BookSet>> GetBooksByPageAsync(int Page, int CountityOnPage)
        {
            return await _context.Books
                .Skip((Page - 1) * CountityOnPage)
                .Take(CountityOnPage)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookSet>> GetBooksByAuthorAsync(int Page, int CountityOnPage, int AuthorId)
        {
            return await _context.Books
                .Where(i => i.AuthorId == AuthorId)
                .Skip((Page - 1) * CountityOnPage)
                .Take(CountityOnPage)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookSet>> GetBooksByPageByGenreAsync(int Page, int CountityOnPage, int GenreId)
        {
            return await _context.Books.Where(i => i.GenreId == GenreId)
            .Skip((Page - 1) * CountityOnPage)
            .Take(CountityOnPage)
            .ToListAsync();
        }

        public async Task<int> GetMagicBookIdAsync()
        {
            return await Task.Run(() => _context.Books.FirstAsync(i => i.IsPromo).Result.Id);
        }

        public async Task<IEnumerable<BookSet>> GetNewBooksAsync()
        {
            return await _context.Books.OrderByDescending(i => i.AddedInDatabase).Take(12).ToListAsync();
        }

        public async Task<BookSet?> GetSingleBookAsync(int BookId)
        {
            return await _context.Books.FirstOrDefaultAsync(i => i.Id == BookId);
        }
    }
}

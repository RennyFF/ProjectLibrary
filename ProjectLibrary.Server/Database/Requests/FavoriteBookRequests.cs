using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ProjectLibrary.Core.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static ProjectLibrary.Server.Database.AppDbContext;

namespace ProjectLibrary.Server.Database.Requests
{
    public interface IFavoriteBookRequests
    {
        Task<bool> CheckIfFavorite(int UserId, int BookId);
        Task ChangeFavoriteBook(int UserId, int BookId, bool Status);
        Task<int> GetFavoriteBooksCountity(double CountityOnPage, int UserId);
        Task<IEnumerable<BookSet>> GetFavoriteBooksByPage(int Page, int CountityOnPage, int UserId);
    }
    public class FavoriteBookRequests : IFavoriteBookRequests
    {
        private readonly AppDbContext _context;

        public FavoriteBookRequests(AppDbContext context)
        {
            _context = context;
        }

        public async Task ChangeFavoriteBook(int UserId, int BookId, bool Status)
        {
            if (Status)
            {
                var NewFavoriteBook = new FavoriteBookSet() { BookId = BookId, UserId = UserId };
                await _context.FavoriteBooks.AddAsync(NewFavoriteBook);
            }
            else
            {
                var FavoriteBook = await _context.FavoriteBooks.FirstOrDefaultAsync(i => i.UserId == UserId && i.BookId == BookId);
                if (FavoriteBook != null)
                {
                    _context.FavoriteBooks.Remove(FavoriteBook);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfFavorite(int UserId, int BookId)
        {
            return await _context.FavoriteBooks.AnyAsync(i => i.UserId == UserId && i.BookId == BookId);
        }

        public async Task<int> GetFavoriteBooksCountity(double CountityOnPage, int UserId)
        {
            return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.FavoriteBooks.Where(i => i.UserId == UserId).CountAsync().Result / CountityOnPage)));
        }

        public async Task<IEnumerable<BookSet>> GetFavoriteBooksByPage(int Page, int CountityOnPage, int UserId)
        {
            return await _context.FavoriteBooks
                .Where(i => i.UserId == UserId).Join(_context.Books,
                  fav => fav.BookId,
                  book => book.Id,
                  (fav, book) => book)
                .Skip((Page - 1) * CountityOnPage)
                .Take(CountityOnPage).ToListAsync();
        }
    }
}

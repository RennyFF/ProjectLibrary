
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static ProjectLibrary.Server.Database.AppDbContext;

namespace ProjectLibrary.Server.Database.Requests
{
    public interface ISearchRequests
    {
        Task<int> GetAuthorPositiveSearchCountityAsync(double CountityOnPage, string SearchString);
        Task<int> GetBooksPositiveSearchCountityAsync(double CountityOnPage, string SearchString);
        Task<int> GetGenresPositiveSearchCountityAsync(double CountityOnPage, string SearchString);
        Task<IEnumerable<GenreSet>> GetGenresPositiveSearchAsync(int Page, double CountityOnPage, string SearchString);
        Task<IEnumerable<BookSet>> GetBooksPositiveSearchAsync(int Page, double CountityOnPage, string SearchString);
        Task<IEnumerable<AuthorSet>> GetAuthorPositiveSearchAsync(int Page, double CountityOnPage, string SearchString);
    }
    public class SearchRequests : ISearchRequests
    {
        private readonly AppDbContext _context;

        public SearchRequests(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorSet>> GetAuthorPositiveSearchAsync(int Page, double CountityOnPage, string SearchString)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                return await _context.Authors
                    .Skip((Page - 1) * Convert.ToInt32(CountityOnPage))
                    .Take(Convert.ToInt32(CountityOnPage))
                    .ToListAsync();
            }

            SearchString = SearchString.ToLower();

            return await _context.Authors
                    .Where(i => i.FirstName.ToLower().Contains(SearchString) ||
                    i.SecondName.ToLower().Contains(SearchString) ||
                    i.PatronomycName.ToLower().Contains(SearchString))
                    .Skip((Page - 1) * Convert.ToInt32(CountityOnPage))
                    .Take(Convert.ToInt32(CountityOnPage))
                    .ToListAsync();
        }

        public async Task<int> GetAuthorPositiveSearchCountityAsync(double CountityOnPage, string SearchString)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Authors.CountAsync().Result / CountityOnPage)));
            }

            SearchString = SearchString.ToLower();

            return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Authors
            .Where(i => i.FirstName.ToLower().Contains(SearchString) ||
            i.SecondName.ToLower().Contains(SearchString) ||
            i.PatronomycName.ToLower().Contains(SearchString))
            .CountAsync().Result / CountityOnPage)));
        }

        public async Task<IEnumerable<BookSet>> GetBooksPositiveSearchAsync(int Page, double CountityOnPage, string SearchString)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                return await _context.Books
                    .Skip((Page - 1) * Convert.ToInt32(CountityOnPage))
                    .Take(Convert.ToInt32(CountityOnPage))
                    .ToListAsync();
            }

            SearchString = SearchString.ToLower();

            return await _context.Books
                .Where(book => book.Title.ToLower().Contains(SearchString) ||
                               _context.Authors.Any(author => author.Id == book.AuthorId &&
                                                              (author.FirstName.ToLower().Contains(SearchString) ||
                                                               author.SecondName.ToLower().Contains(SearchString) ||
                                                               (author.PatronomycName != null && author.PatronomycName.ToLower().Contains(SearchString)))) ||
                               _context.Genres.Any(genre => genre.Id == book.GenreId &&
                                                            genre.GenreName.ToLower().Contains(SearchString)))
                .Skip((Page - 1) * Convert.ToInt32(CountityOnPage))
                .Take(Convert.ToInt32(CountityOnPage))
                .ToListAsync();
        }

        public async Task<int> GetBooksPositiveSearchCountityAsync(double CountityOnPage, string SearchString)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Books.CountAsync().Result / CountityOnPage)));
            }

            SearchString = SearchString.ToLower();

            return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Books
                .Where(book => book.Title.ToLower().Contains(SearchString) ||
                               _context.Authors.Any(author => author.Id == book.AuthorId &&
                                                              (author.FirstName.ToLower().Contains(SearchString) ||
                                                               author.SecondName.ToLower().Contains(SearchString) ||
                                                               (author.PatronomycName != null && author.PatronomycName.ToLower().Contains(SearchString)))) ||
                               _context.Genres.Any(genre => genre.Id == book.GenreId &&
                                                            genre.GenreName.ToLower().Contains(SearchString))).CountAsync().Result / CountityOnPage)));
        }

        public async Task<IEnumerable<GenreSet>> GetGenresPositiveSearchAsync(int Page, double CountityOnPage, string SearchString)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                return await _context.Genres
                .Skip((Page - 1) * Convert.ToInt32(CountityOnPage))
                .Take(Convert.ToInt32(CountityOnPage))
                .ToListAsync();
            }

            SearchString = SearchString.ToLower();

            return await _context.Genres
            .Where(i => i.GenreName.ToLower().Contains(SearchString))
            .Skip((Page - 1) * Convert.ToInt32(CountityOnPage))
            .Take(Convert.ToInt32(CountityOnPage))
            .ToListAsync();
        }

        public async Task<int> GetGenresPositiveSearchCountityAsync(double CountityOnPage, string SearchString)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Genres.CountAsync().Result / CountityOnPage)));
            }

            SearchString = SearchString.ToLower();

            return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Genres
            .Where(i => i.GenreName.ToLower().Contains(SearchString)).CountAsync().Result / CountityOnPage)));
        }
    }
}

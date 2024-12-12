using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ProjectLibrary.Core.Types;
using System.Collections.ObjectModel;
using static ProjectLibrary.Server.Database.AppDbContext;

namespace ProjectLibrary.Server.Database.Requests
{
    public interface IAuthorRequests
    {
        Task<int> GetAuthorCountityAsync(double CountityOnPage);
        Task<IEnumerable<AuthorSet>> GetAuthorsByPageAsync(int Page, int CountityOnPage);
        Task<IEnumerable<AuthorSet>> GetAllAuthorsAsync();
        Task<AuthorSet?> GetSingleAuthorAsync(int AuthorId);
        Task<string> GetShortAuthorName(int AuthorId);
    }
    public class AuthorRequests : IAuthorRequests
    {
        private readonly AppDbContext _context;

        public AuthorRequests(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorSet>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<int> GetAuthorCountityAsync(double CountityOnPage)
        {
            return await Task.Run(() => Convert.ToInt32(Math.Ceiling(_context.Authors.CountAsync().Result / CountityOnPage)));
        }
        public async Task<IEnumerable<AuthorSet>> GetAuthorsByPageAsync(int Page, int CountityOnPage)
        {
            return await _context.Authors
            .Skip((Page - 1) * CountityOnPage)
            .Take(CountityOnPage)
            .ToListAsync();
        }

        public async Task<AuthorSet?> GetSingleAuthorAsync(int AuthorId)
        {
            return await _context.Authors.FirstOrDefaultAsync(i => i.Id == AuthorId);
        }

        public async Task<string> GetShortAuthorName(int AuthorId)
        {
            var Author = await _context.Authors.FirstOrDefaultAsync(i => i.Id == AuthorId);
            if (Author == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Автор не найден в базе данных"));
            }
            return $"{Author.SecondName} {Author.FirstName[0]}. {Author.PatronomycName[0]}.";
        }
    }
}

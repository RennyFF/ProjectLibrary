using Microsoft.EntityFrameworkCore;
using Npgsql;
using ProjectLibrary.Core.Converters;
using static ProjectLibrary.Server.Database.AppDbContext;

namespace ProjectLibrary.Server.Database.Requests
{
    public interface IOrderRequests
    {
        Task<bool> CheckIfOwned(int UserId, int BookId);
        Task AddOrder(int UserId, int BookId, bool IsFromPromo);
    }
    public class OrderRequests : IOrderRequests
    {
        private readonly AppDbContext _context;

        public OrderRequests(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrder(int UserId, int BookId, bool IsFromPromo)
        {
            var NewOrder = new OrderSet()
            {
                BookId = BookId,
                UserId = UserId,
                IsFromPromo = IsFromPromo,
                OrderDate = UnixTimeConverter.DateTimeToTimeStamp(DateTime.Now)
            };
            await _context.Orders.AddAsync(NewOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfOwned(int UserId, int BookId)
        {
            return await _context.Orders.AnyAsync(i=> i.UserId == UserId && i.BookId == BookId);
        }
    }
}

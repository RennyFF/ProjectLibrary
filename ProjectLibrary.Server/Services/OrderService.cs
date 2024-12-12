using Grpc.Core;
using ProjectLibrary.Core.Converters;
using ProjectLibrary.Server.Database;
using ProjectLibrary.Server.Database.Requests;
using ProjectLibrary.Server.Order;
using static ProjectLibrary.Server.Database.AppDbContext;

namespace ProjectLibrary.Server.Services
{
    public class OrderService : Order.OrderService.OrderServiceBase
    {
        private readonly IOrderRequests _orderRequests;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IOrderRequests OrderRequests, ILogger<OrderService> logger)
        {
            _orderRequests = OrderRequests;
            _logger = logger;
        }
        public override Task<ResponsePlaceOrder> PlaceOrder(RequestPlaceOrder request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "PlaceOrder - success");
            try
            {
                OrderSet NewOrder = new OrderSet()
                {
                    BookId = request.BookId,
                    UserId = request.UserId,
                    IsFromPromo = request.IsPromo,
                    OrderDate = UnixTimeConverter.DateTimeToTimeStamp(DateTime.Now)
                };
            }
            catch (Exception)
            {
                throw new RpcException(new Status(StatusCode.Aborted, "Произошла ошибка"));
            }
            return Task.FromResult(new ResponsePlaceOrder()
            { IsSuccess = true }
            );
        }
        public async override Task<ResponseIsBought> IsBought(RequestIsBought request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "IsBought()");
            var Result = new ResponseIsBought()
            {
                IsOrdered = await _orderRequests.CheckIfOwned(request.UserId, request.BookId)
            };
            return await Task.FromResult(Result);
        }
    }
}

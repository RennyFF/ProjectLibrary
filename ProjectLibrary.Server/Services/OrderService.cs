using Grpc.Core;
using Newtonsoft.Json;
using ProjectLibrary.Core.Converters;
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
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented)}");
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
            var Result = new ResponsePlaceOrder(){
                IsSuccess =true
            };
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented)}");
            return Task.FromResult(Result);
        }
        public async override Task<ResponseIsBought> IsBought(RequestIsBought request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented)}");
            var Result = new ResponseIsBought()
            {
                IsOrdered = await _orderRequests.CheckIfOwned(request.UserId, request.BookId)
            };
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented)}");
            return await Task.FromResult(Result);
        }
    }
}

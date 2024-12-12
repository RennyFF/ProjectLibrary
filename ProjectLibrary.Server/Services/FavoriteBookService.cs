using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProjectLibrary.Core.Converters;
using ProjectLibrary.Server.Database.Requests;
using ProjectLibrary.Server.FavoriteBook;

namespace ProjectLibrary.Server.Services
{
    public class FavoriteBookService : FavoriteBook.FavoriteBookService.FavoriteBookServiceBase
    {
        private readonly IFavoriteBookRequests _favBookRequests;
        private readonly IAuthorRequests _authorRequests;
        private readonly ILogger<FavoriteBookService> _logger;
        public FavoriteBookService(IFavoriteBookRequests FavoriteBookRequests, ILogger<FavoriteBookService> logger, IAuthorRequests authorRequests)
        {
            _favBookRequests = FavoriteBookRequests;
            _logger = logger;
            _authorRequests = authorRequests;
        }
        public override async Task<Empty> ChangeFavoriteBook(RequestChangeFavoriteBook request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "ChangeFavoriteBook()");
            await _favBookRequests.ChangeFavoriteBook(request.UserId, request.BookId, request.Status);
            return await Task.FromResult(new Empty());
        }
        public override async Task<ResponseCheckFavoriteBook> CheckFavoriteBook(RequestCheckFavoriteBook request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "CheckFavoriteBook()");
            var Result = new ResponseCheckFavoriteBook()
            {
                IsFavorite = await _favBookRequests.CheckIfFavorite(request.UserId, request.BookId)
            };
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseCountity> GetCountity(RequestCountity request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetCountity()");
            var Result = new ResponseCountity() { Countity = await _favBookRequests.GetFavoriteBooksCountity(request.CountityOnPage, request.UserId) };
            return await Task.FromResult(Result);
        }
        public async override Task<ResponseFavoriteBookByUser> GetFavoriteBooksByUser(RequestFavoriteBookByUser request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetFavBooksByUser()");
            var Result = new ResponseFavoriteBookByUser();
            var FavBooks = await _favBookRequests.GetFavoriteBooksByPage(request.Page, request.CountityOnPage, request.UserId);
            if (FavBooks == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Нету избранных книг!"));
            }
            Result.FavoriteBooks.AddRange(FavBooks.Select(i => new BookCard()
            {
                Id = i.Id,
                Title = i.Title,
                AuthorFullnameShort = _authorRequests.GetShortAuthorName(i.AuthorId).Result,
                Image = Google.Protobuf.ByteString.CopyFrom(i.Image),
                RatingStars = i.RatingStars,
                AddedInDatabase = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(UnixTimeConverter.TimeStampToDateTime(i.AddedInDatabase))
            }
            ));
            return await Task.FromResult(Result);
        }
    }
}

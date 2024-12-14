using Grpc.Core;
using ProjectLibrary.Server.Database.Requests;

namespace ProjectLibrary.Server.Services
{
    public class SearchService : Search.SearchService.SearchServiceBase
    {
        private readonly ISearchRequests _searchRequests;
        private readonly IAuthorRequests _authorRequests;
        private readonly ILogger<SearchService> _logger;
        public SearchService(ISearchRequests SearchRequests, ILogger<SearchService> logger, IAuthorRequests authorRequests)
        {
            _searchRequests = SearchRequests;
            _logger = logger;
            _authorRequests = authorRequests;
        }
        public async override Task<Search.ResAuthorsPagination> SearchAuthorPagination(Search.ReqPagination request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "SearchAuthorPagination()");
            var Result = new Search.ResAuthorsPagination();
            var Authors = await _searchRequests.GetAuthorPositiveSearchAsync(request.Page, request.CountityOnPage, request.SearchString);
            if (Authors == null)
            {
                throw new RpcException(new Status(StatusCode.Aborted, "Ошибка в получении авторов!"));
            }
            Result.AuthorCards.AddRange(Authors.Select(i => new Search.AuthorCard()
            {
                Id = i.Id,
                AuthorFullnameShort = $"{i.SecondName} {i.FirstName} {i.PatronomycName}",
                Image = Google.Protobuf.ByteString.CopyFrom(i.ImageAvatar)
            }));
            return await Task.FromResult(Result);
        }
        public async override Task<Search.ResBooksPagination> SearchBookPagination(Search.ReqPagination request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "SearchBookPagination()");
            var Result = new Search.ResBooksPagination();
            var Books = await _searchRequests.GetBooksPositiveSearchAsync(request.Page, request.CountityOnPage, request.SearchString);
            if (Books == null)
            {
                throw new RpcException(new Status(StatusCode.Aborted, "Ошибка в получении книг с сервера!"));
            }
            Result.BookCards.AddRange(Books.Select(i => new Search.BookCard()
            {
                Id = i.Id,
                Title = i.Title,
                AuthorFullnameShort = _authorRequests.GetShortAuthorName(i.AuthorId).Result,
                Image = Google.Protobuf.ByteString.CopyFrom(i.Image),
                RatingStars = i.RatingStars,
            }
            ));
            return await Task.FromResult(Result);
        }
        public async override Task<Search.ResGenresPagination> SearchGenrePagination(Search.ReqPagination request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "SearchGenrePagination()");
            var Result = new Search.ResGenresPagination();
            var Genres = await _searchRequests.GetGenresPositiveSearchAsync(request.Page, request.CountityOnPage, request.SearchString);
            if (Genres == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Ошибка в получении жанров!"));
            }
            Result.GenreCards.AddRange(Genres.Select(i => new Search.GenreCard()
            {
                Id = i.Id,
                GenreName = i.GenreName,
                Image = Google.Protobuf.ByteString.CopyFrom(i.ImageAvatar)
            }));
            return await Task.FromResult(Result);
        }

        public override async Task<Search.ResSCountity> SearchCountityAuthor(Search.ReqSCountity request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "SearchCountityAuthor()");
            var Result = new Search.ResSCountity()
            {
                Countity = await _searchRequests.GetAuthorPositiveSearchCountityAsync(request.CountityOnPage, request.SearchString)
            };
            return await Task.FromResult(Result);
        }
        public async override Task<Search.ResSCountity> SearchCountityBook(Search.ReqSCountity request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "SearchCountityBook()");
            var Result = new Search.ResSCountity()
            {
                Countity = await _searchRequests.GetBooksPositiveSearchCountityAsync(request.CountityOnPage, request.SearchString)
            };
            return await Task.FromResult(Result);
        }
        public async override Task<Search.ResSCountity> SearchCountityGenre(Search.ReqSCountity request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "SearchCountityGenre()");
            var Result = new Search.ResSCountity()
            {
                Countity = await _searchRequests.GetGenresPositiveSearchCountityAsync(request.CountityOnPage, request.SearchString)
            };
            return await Task.FromResult(Result);
        }
    }
}

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProjectLibrary.Core.Converters;
using ProjectLibrary.Core.Types;
using ProjectLibrary.Server.Database.Requests;
using ProjectLibrary.Server.Genre;

namespace ProjectLibrary.Server.Services
{
    public class GenreService : Genre.GenreService.GenreServiceBase
    {
        private readonly IGenreRequests _genreRequests;
        private readonly ILogger<GenreService> _logger;
        public GenreService(IGenreRequests GenreRequests, ILogger<GenreService> logger)
        {
            _genreRequests = GenreRequests;
            _logger = logger;
        }
        public async override Task<ResponseGenreNames> GetAllGenreNames(Empty request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetAllGenreNames()");
            var Result = new ResponseGenreNames();
            var genreCards = await _genreRequests.GetAllGenresAsync();
            Result.Genres.AddRange(genreCards.Select(i => new GenreNameAndId()
            { 
                Id = i.Id,
                GenreName = i.GenreName
            }));
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseCountity> GetCountity(RequestCountity request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetCountity()");
            var Result = new ResponseCountity() { Countity = await _genreRequests.GetGenreCountityAsync(request.CountityOnPage) };
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseGenresByPage> GetGenresByPage(RequestGenresByPage request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetGenresByPage()");
            var Result = new ResponseGenresByPage();
            var GenreCardsByPage = await _genreRequests.GetGenresByPageAsync(request.Page, request.CountityOnPage);
            if (GenreCardsByPage == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Ошибка в получении жанров!"));
            }
            Result.Genres.AddRange(GenreCardsByPage.Select(i => new ResponseSingleGenre()
            {
                Id = i.Id,
                GenreName = i.GenreName,
                Image = Google.Protobuf.ByteString.CopyFrom(i.ImageAvatar)
            }));
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseSingleGenre> GetSingleGenre(RequestSingleGenre request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetSingleGenre()");
            var FullGenre = await _genreRequests.GetSingleGenreAsync(request.GenreId);
            if (FullGenre == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Жанр не найден в базе данных"));
            }
            var Result = new ResponseSingleGenre()
            {
                Id = FullGenre.Id,
                GenreName = FullGenre.GenreName,
                Image = Google.Protobuf.ByteString.CopyFrom(FullGenre.ImageAvatar)
            };
            return await Task.FromResult(Result);
        }
    }
}

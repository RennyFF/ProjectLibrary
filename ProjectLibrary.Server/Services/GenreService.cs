using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Newtonsoft.Json;
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
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented)}");
            var Result = new ResponseGenreNames();
            var genreCards = await _genreRequests.GetAllGenresAsync();
            Result.Genres.AddRange(genreCards.Select(i => new GenreNameAndId()
            { 
                Id = i.Id,
                GenreName = i.GenreName
            }));
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented)}");
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseCountity> GetCountity(RequestCountity request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented)}");
            var Result = new ResponseCountity() { Countity = await _genreRequests.GetGenreCountityAsync(request.CountityOnPage) };
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented)}");
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseGenresByPage> GetGenresByPage(RequestGenresByPage request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented)}");
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
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: Genres");
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseSingleGenre> GetSingleGenre(RequestSingleGenre request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented)}");
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
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: Genre");
            return await Task.FromResult(Result);
        }
    }
}

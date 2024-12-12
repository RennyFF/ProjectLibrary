using Grpc.Core;
using ProjectLibrary.Core.Converters;
using ProjectLibrary.Server.Author;
using ProjectLibrary.Server.Database.Requests;
using ProjectLibrary.Server.Genre;

namespace ProjectLibrary.Server.Services
{
    public class AuthorService : Author.AuthorService.AuthorServiceBase
    {
        private readonly IAuthorRequests _authorRequests;
        private readonly ILogger<AuthorService> _logger;
        public AuthorService(IAuthorRequests AuthorRequests, ILogger<AuthorService> logger)
        {
            _authorRequests = AuthorRequests;
            _logger = logger;
        }
        public async override Task<ResponseAuthorsByPage> GetAuthorsByPage(RequestAuthorsByPage request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetAuthorsByPage()");
            var AuthorCardsByPage = await _authorRequests.GetAuthorsByPageAsync(request.Page, request.CountityOnPage);
            if (AuthorCardsByPage == null)
            {
                throw new RpcException(new Status(StatusCode.Aborted, "Ошибка в получении авторов!"));
            }
            var Result = new ResponseAuthorsByPage();
            Result.Authors.AddRange(AuthorCardsByPage.Select(i => new AuthorCard()
            {
                Id = i.Id,
                AuthorFullnameShort = $"{i.SecondName} {i.FirstName} {i.PatronomycName}",
                Image = Google.Protobuf.ByteString.CopyFrom(i.ImageAvatar)
            }));
            return await Task.FromResult(Result);
        }
        public async override Task<Author.ResponseCountity> GetCountity(Author.RequestCountity request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetCountity()");
            var Result = new Author.ResponseCountity()
            {
                Countity = await _authorRequests.GetAuthorCountityAsync(request.CountityOnPage)
            };
            return await Task.FromResult(Result);
        }
        public async override Task<ResponseSingleAuthor> GetSingleAuthor(RequestSingleAuthor request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetSingleAuthor()");
            var SingleAuthor = await _authorRequests.GetSingleAuthorAsync(request.AuthorId);
            if (SingleAuthor == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Автор не найден в базе данных"));
            }
            var Result = new ResponseSingleAuthor()
            {
                Id = SingleAuthor.Id,
                FirstName = SingleAuthor.FirstName,
                SecondName = SingleAuthor.SecondName,
                PatronomycName = SingleAuthor.PatronomycName,
                DateOfDeath = SingleAuthor.DateOfDeath != null ? Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(UnixTimeConverter.TimeStampToDateTime((long)SingleAuthor.DateOfDeath)) : null,
                DateOfBirth = SingleAuthor.DateOfBirthday != null ? Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(UnixTimeConverter.TimeStampToDateTime((long)SingleAuthor.DateOfBirthday)) : null,
                Image = Google.Protobuf.ByteString.CopyFrom(SingleAuthor.ImageAvatar)
            };
            return await Task.FromResult(Result);
        }
    }
}

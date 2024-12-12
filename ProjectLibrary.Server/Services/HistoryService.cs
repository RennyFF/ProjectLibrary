using Grpc.Core;
using ProjectLibrary.Server.Database.Requests;
using ProjectLibrary.Server.History;
using System.Linq;
using static ProjectLibrary.Server.Database.AppDbContext;

namespace ProjectLibrary.Server.Services
{
    public class HistoryService : History.HistoryService.HistoryServiceBase
    {
        private readonly IGenreRequests _genreRequests;
        private readonly IAuthorRequests _authorRequests;
        private readonly IBookRequests _bookRequests;
        private readonly ILogger<GenreService> _logger;
        public HistoryService(IGenreRequests GenreRequests, ILogger<GenreService> logger, IAuthorRequests AuthorRequests, IBookRequests BookRequests)
        {
            _genreRequests = GenreRequests;
            _authorRequests = AuthorRequests;
            _bookRequests = BookRequests;
            _logger = logger;
        }
        public override async Task<ResponseHistory> GetHistoryCards(RequestHistory request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetHistoryCards()");
            var Result = new ResponseHistory();
            List<AuthorSet> authorCards = new List<AuthorSet>();
            List<BookSet> bookCards = new List<BookSet>();
            List<GenreSet> genreCards = new List<GenreSet>();
            foreach (var item in request.Authors)
            {
                var _ = await _authorRequests.GetSingleAuthorAsync(item.Id);
                if (_ == null)
                {
                    continue;
                }
                authorCards.Add(_);
            }
            foreach (var item in request.Books)
            {
                var _ = await _bookRequests.GetSingleBookAsync(item.Id);
                if (_ == null)
                {
                    continue;
                }
                bookCards.Add(_);
            }
            foreach (var item in request.Genres)
            {
                var _ = await _genreRequests.GetSingleGenreAsync(item.Id);
                if (_ == null)
                {
                    continue;
                }
                genreCards.Add(_);
            }
            Result.Authors.AddRange(authorCards.Select(i => new AuthorCardHistory()
            {
                Id = i.Id,
                AuthorFullname = $"{i.SecondName} {i.FirstName} {i.PatronomycName}",
                Image = Google.Protobuf.ByteString.CopyFrom(i.ImageAvatar)
            }));
            Result.Books.AddRange(bookCards.Select(i => new BookCardHistory()
            {
                Id = i.Id,
                AuthorFullnameShort = _authorRequests.GetShortAuthorName(i.AuthorId).Result,
                Image = Google.Protobuf.ByteString.CopyFrom(i.Image),
                RatingStars = i.RatingStars,
                Title = i.Title
            }));
            Result.Genres.AddRange(genreCards.Select(i => new GenreCardHistory()
            {
                Id = i.Id,
                GenreName = i.GenreName,
                Image = Google.Protobuf.ByteString.CopyFrom(i.ImageAvatar)
            }));
            return await Task.FromResult(Result);
        }
    }
}

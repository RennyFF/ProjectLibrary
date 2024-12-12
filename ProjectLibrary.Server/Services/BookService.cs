using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProjectLibrary.Core.Converters;
using ProjectLibrary.Server.Author;
using ProjectLibrary.Server.Book;
using ProjectLibrary.Server.Database.Requests;

namespace ProjectLibrary.Server.Services
{
    public class BookService : Book.BookService.BookServiceBase
    {
        private readonly IBookRequests _bookRequests;
        private readonly IAuthorRequests _authorRequests;
        private readonly IGenreRequests _genreRequests;
        private readonly ILogger<BookService> _logger;
        public BookService(IBookRequests BookRequests, IAuthorRequests authorRequests, ILogger<BookService> logger, IGenreRequests genreRequests)
        {
            _bookRequests = BookRequests;
            _authorRequests = authorRequests;
            _logger = logger;
            _genreRequests = genreRequests;
        }
        public async override Task<ResponseBooksByAuthor> GetAuthorsBooks(RequestBooksByAuthor request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetAuthorsBooks - success");
            var Result = new ResponseBooksByAuthor();
            var NewBooks = await _bookRequests.GetBooksByAuthorAsync(request.Page, request.CountityOnPage, request.AuthorId);
            Result.Books.AddRange(NewBooks.Select(i => new BookCard()
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
        public async override Task<ResponseBestsellerBooks> GetBestSellerBooks(Empty request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetBestSellerBooks()");
            var Result = new ResponseBestsellerBooks();
            var BestsellerBooks = await _bookRequests.GetBestSellerBooksAsync();
            if (BestsellerBooks == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Нету книг-бестселлеров!"));
            }
            Result.Books.AddRange(BestsellerBooks.Select(i => new BookCard()
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
        public async override Task<ResponseBooksByPage> GetBooksByPage(RequestBooksByPage request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetBooksByPage()");
            var Result = new ResponseBooksByPage();
            var NewBooks = request.GenreId == null ? await _bookRequests.GetBooksByPageAsync(request.Page, request.CountityOnPage) : await _bookRequests.GetBooksByPageByGenreAsync(request.Page, request.CountityOnPage, (int)request.GenreId);
            if (NewBooks == null)
            {
                throw new RpcException(new Status(StatusCode.Aborted, "Ошибка в получении книг с сервера!"));
            }
            Result.Books.AddRange(NewBooks.Select(i => new BookCard()
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
        public override async Task<Book.ResponseCountity> GetCountity(Book.RequestCountity request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetCountity()");
            var Result = new Book.ResponseCountity();
            if (request.GenreId == null && request.AuthorId == null)
            {
                Result.Countity = await _bookRequests.GetBookCountityAsync(request.CountityOnPage);
            }
            else if (request.GenreId == null && request.AuthorId != null)
            {
                Result.Countity = await _bookRequests.GetBookCountityByAuthorAsync(request.CountityOnPage, (int)request.AuthorId);
            }
            else if (request.GenreId != null && request.AuthorId == null)
            {
                Result.Countity = await _bookRequests.GetBookCountityByGenreAsync(request.CountityOnPage, (int)request.GenreId);
            }
            if (Result == null) {
                throw new RpcException(new Status(StatusCode.Aborted, "Ошибка в получении количества книг с сервера!"));
            }
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseFullBook> GetFullBook(RequestFullBook request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetFullBook()");
            var FullBook = await _bookRequests.GetSingleBookAsync(request.BookId);
            if (FullBook == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Книга не найдена в базе данных"));
            }
            var BookAuthor = await _authorRequests.GetSingleAuthorAsync(FullBook.AuthorId);
            var BookGenre = await _genreRequests.GetSingleGenreAsync(FullBook.GenreId);
            if (BookAuthor == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Автор не найден в базе данных"));
            }
            else if(BookGenre == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Жанр не найден в базе данных"));
            }
            var Result = new ResponseFullBook()
            {
                Id = FullBook.Id,
                PublicationDate = Timestamp.FromDateTime(UnixTimeConverter.TimeStampToDateTime(FullBook.PublicationDate)),
                PageCount = FullBook.PagesCout,
                Image = Google.Protobuf.ByteString.CopyFrom(FullBook.Image),
                Title = FullBook.Title,
                RatingStars = FullBook.RatingStars,
                IsBestseller = FullBook.IsBestSeller,
                Description = FullBook.Description,
                Price = Decimal.ToDouble(FullBook.Price),
                IsPromo = FullBook.IsPromo,
                Author = new FullBookAuthor() {  AuthorFullname = $"{BookAuthor.SecondName} {BookAuthor.FirstName} {BookAuthor.PatronomycName}", Id = BookAuthor.Id },
                Genre = new FullBookGenre() { Id = BookGenre.Id, GenreName = BookGenre.GenreName }
            };
            return await Task.FromResult(Result);
        }

        public override async Task<ResponseMagicBookId> GetMagicBookId(Empty request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetMagicBookId()");
            var Result = new ResponseMagicBookId()
            {
                BookId = await _bookRequests.GetMagicBookIdAsync()
            };
            if (Result == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Книга не найдена в базе данных"));
            }
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseNewBooks> GetNewBooks(Empty request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "GetNewBooks()");
            var Result = new ResponseNewBooks();
            var NewBooks = await _bookRequests.GetNewBooksAsync();
            if (NewBooks == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Нету новых книг!"));
            }
            Result.Books.AddRange(NewBooks.Select(i => new BookCard()
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

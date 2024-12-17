using Grpc.Core;
using Newtonsoft.Json;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.Server.Database.Requests;
using ProjectLibrary.Server.User;

namespace ProjectLibrary.Server.Services
{
    public class UserService : User.UserService.UserServiceBase
    {
        private readonly IUserRequests _userRequests;
        private readonly IFavoriteGenreRequests _favGenreRequests;
        private readonly IGenreRequests _genreRequests;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRequests UserRequests, ILogger<UserService> logger, IFavoriteGenreRequests FavoriteGenreRequests, IGenreRequests GenreRequests)
        {
            _userRequests = UserRequests;
            _favGenreRequests = FavoriteGenreRequests;
            _genreRequests = GenreRequests;
            _logger = logger;
        }

        public override async Task<ResponseAuthorize> AuthorizeUser(RequestAuthorize request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented)}");
            var AuthorizedUser = await _userRequests.GetSingleUser(request.Login, request.PasswordHash);
            if (AuthorizedUser == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Пользователь не найден!"));
            }
            var FavGenres = await _favGenreRequests.GetFavGenreByUserAsync(AuthorizedUser.Id);
            var Result = new ResponseAuthorize()
            {
                Id = AuthorizedUser.Id,
                FirstName = AuthorizedUser.FirstName,
                SecondName = AuthorizedUser.SecondName,
                PatronomycName = AuthorizedUser.PatronomycName
            };
            if (FavGenres == null)
            {
                Result.FavoriteGenres.AddRange(null);
            }
            else
            {
                Result.FavoriteGenres.AddRange(
                    FavGenres.Select(i => new FavoriteGenre() { GenreId = i.GenreId, GenreName = _genreRequests.GetGenreNameByIdAsync(i.GenreId).Result, ClickedCountity = i.ClickedCountity })
                    );
            }
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented)}");
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseRegister> RegisterUser(RequestRegister request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented)}");
            try
            {
                await _userRequests.AddUser(new UserType()
                {
                    FirstName = request.FirstName,
                    SecondName = request.SecondName,
                    PatronomycName = request.PatronomycName,
                    Email = request.Email,
                    Login = request.Login,
                    PasswordHash = request.PasswordHash,
                    BirthdayDate = request.BirthdayDate.ToDateTime().ToLocalTime()
                });
            }
            catch (Exception)
            {
                throw new RpcException(new Status(StatusCode.Aborted, "Произошла ошибка"));
            }
            var Result = new ResponseRegister() { IsSuccess = true };
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented)}");
            return await Task.FromResult(Result);
        }
        public override async Task<ResponseCheckUnique> CheckUniqueField(RequestCheckUnique request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented)}");
            var Result = new ResponseCheckUnique()
            {
                EmailIsUnique = await _userRequests.CheckIfUniqueEmail(request.ValidStringEmail),
                LoginIsUnique = await _userRequests.CheckIfUniqueLogin(request.ValidStringLogin),
            };
            _logger.Log(LogLevel.Information, $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm:ss]")} GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented)}");
            return await Task.FromResult(Result);
        }
    }
}

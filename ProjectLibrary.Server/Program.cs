using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectLibrary.Server.Database;
using ProjectLibrary.Server.Database.Requests;
using ProjectLibrary.Server.Services;

namespace ProjectLibrary.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            try
            {
                builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("ProjectLibraryDB"), x =>
                {
                    x.MigrationsHistoryTable("__EFMigrationsHistory", "ProjectLibrary");
                }));
            }
            catch (Exception)
            {
                Console.WriteLine("Database configure failed");
            }
            finally
            {
                Console.WriteLine("Database configured succsesfully");
            }
            builder.Services.AddGrpc();
            builder.Services.AddScoped<IAuthorRequests, AuthorRequests>();
            builder.Services.AddScoped<IBookRequests, BookRequests>();
            builder.Services.AddScoped<IFavoriteBookRequests, FavoriteBookRequests>();
            builder.Services.AddScoped<IFavoriteGenreRequests, FavoriteGenreRequests>();
            builder.Services.AddScoped<IGenreRequests, GenreRequests>();
            builder.Services.AddScoped<IOrderRequests, OrderRequests>();
            builder.Services.AddScoped<IUserRequests, UserRequests>();
            var app = builder.Build();
            app.MapGrpcService<UserService>();
            app.MapGrpcService<OrderService>();
            app.MapGrpcService<GenreService>();
            app.MapGrpcService<FavoriteBookService>();
            app.MapGrpcService<AuthorService>();
            app.MapGrpcService<BookService>();
            app.MapGrpcService<HistoryService>();
            app.MapGet("/", () => "Exit this page please");
            app.Run();
        }
    }
}
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjectLibrary.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ProjectLibrary");

            migrationBuilder.CreateTable(
                name: "Authors",
                schema: "ProjectLibrary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SecondName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    PatronomycName = table.Column<string>(type: "text", nullable: false),
                    ImageAvatar = table.Column<byte[]>(type: "bytea", nullable: false),
                    DateOfBirthday = table.Column<long>(type: "bigint", nullable: true),
                    DateOfDeath = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "ProjectLibrary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false),
                    PublicationDate = table.Column<long>(type: "bigint", nullable: false),
                    PagesCout = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    AddedInDatabase = table.Column<long>(type: "bigint", nullable: false),
                    RatingStars = table.Column<int>(type: "integer", nullable: false),
                    IsBestSeller = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    IsPromo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteBooks",
                schema: "ProjectLibrary",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteBooks", x => new { x.UserId, x.BookId });
                });

            migrationBuilder.CreateTable(
                name: "FavoriteGenres",
                schema: "ProjectLibrary",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false),
                    ClickedCountity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteGenres", x => new { x.UserId, x.GenreId });
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                schema: "ProjectLibrary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GenreName = table.Column<string>(type: "text", nullable: false),
                    ImageAvatar = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "ProjectLibrary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    OrderDate = table.Column<long>(type: "bigint", nullable: false),
                    IsFromPromo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "ProjectLibrary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    SecondName = table.Column<string>(type: "text", nullable: false),
                    PatronomycName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    BirthdayDate = table.Column<long>(type: "bigint", nullable: false),
                    DateOfCreation = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdated = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors",
                schema: "ProjectLibrary");

            migrationBuilder.DropTable(
                name: "Books",
                schema: "ProjectLibrary");

            migrationBuilder.DropTable(
                name: "FavoriteBooks",
                schema: "ProjectLibrary");

            migrationBuilder.DropTable(
                name: "FavoriteGenres",
                schema: "ProjectLibrary");

            migrationBuilder.DropTable(
                name: "Genres",
                schema: "ProjectLibrary");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "ProjectLibrary");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "ProjectLibrary");
        }
    }
}

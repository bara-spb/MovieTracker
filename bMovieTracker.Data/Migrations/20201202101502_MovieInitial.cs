using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bMovieTracker.Data.Migrations
{
    public partial class MovieInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Rate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Genre", "Rate", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "Drama", "FiveStars", "The Green Mile", 1999 },
                    { 2, "Fantasy", "FiveStars", "Interstellar", 2014 },
                    { 3, "Animation", "FiveStars", "The Lion King", 1994 },
                    { 4, "Fantasy", "FourStars", "The Matrix", 1999 },
                    { 5, "Action", "ThreeStars", "Terminator Salvation", 2009 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}

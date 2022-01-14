using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactMovieApi.Migrations
{
    public partial class fixedRealeseDateInMovieTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "Movies",
                newName: "ReleaseDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Movies",
                newName: "dateTime");
        }
    }
}

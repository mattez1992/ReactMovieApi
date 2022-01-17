using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactMovieApi.Migrations
{
    public partial class changedCharacterToMovieCharacterForActorAndMovieActorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Character",
                table: "MovieActors",
                newName: "MovieCharacter");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieCharacter",
                table: "MovieActors",
                newName: "Character");
        }
    }
}

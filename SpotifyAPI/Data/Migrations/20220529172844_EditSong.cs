using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyAPI.Migrations
{
    public partial class EditSong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Songs");

            migrationBuilder.AddColumn<bool>(
                name: "IsExplicit",
                table: "Songs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SongUrl",
                table: "Songs",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExplicit",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "SongUrl",
                table: "Songs");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Songs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

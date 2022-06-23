using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyAPI.Migrations
{
    public partial class removeSOngUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongUrl",
                table: "Songs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SongUrl",
                table: "Songs",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");
        }
    }
}

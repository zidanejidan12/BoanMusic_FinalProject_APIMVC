using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyAPI.Migrations
{
    public partial class addMp3ToSongs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "SongMP3",
                table: "Songs",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongMP3",
                table: "Songs");
        }
    }
}

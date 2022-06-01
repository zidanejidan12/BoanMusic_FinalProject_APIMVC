using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyAPI.Migrations
{
    public partial class editedArtistImageSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Artists");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Artists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Artists");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Artists",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}

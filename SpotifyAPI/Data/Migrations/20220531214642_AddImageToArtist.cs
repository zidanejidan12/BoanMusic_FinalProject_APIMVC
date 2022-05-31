using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyAPI.Migrations
{
    public partial class AddImageToArtist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Artists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Artists");
        }
    }
}

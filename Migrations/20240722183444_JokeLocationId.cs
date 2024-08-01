using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunnyMaps.Server.Migrations
{
    public partial class JokeLocationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "Jokes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Jokes");
        }
    }
}

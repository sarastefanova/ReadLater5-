using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddfieldnumberClickedSavedBookmarksPerUsertableandremovenumberclickedfromApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberClicked",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "NumberClicked",
                table: "SavedBookmarksPerUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberClicked",
                table: "SavedBookmarksPerUser");

            migrationBuilder.AddColumn<int>(
                name: "NumberClicked",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

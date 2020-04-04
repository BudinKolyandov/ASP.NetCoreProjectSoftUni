namespace BugTracker.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RenamedRealNameToFullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "RealName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

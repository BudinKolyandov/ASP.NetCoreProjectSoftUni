namespace BugTracker.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedNameToBugEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Bugs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Bugs");
        }
    }
}

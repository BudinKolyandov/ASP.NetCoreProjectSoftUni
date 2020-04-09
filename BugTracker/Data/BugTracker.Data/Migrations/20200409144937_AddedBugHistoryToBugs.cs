namespace BugTracker.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedBugHistoryToBugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "BugsHistories");

            migrationBuilder.DropColumn(
                name: "OldValue",
                table: "BugsHistories");

            migrationBuilder.AddColumn<string>(
                name: "NewDescriptionValue",
                table: "BugsHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldDescriptionValue",
                table: "BugsHistories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewDescriptionValue",
                table: "BugsHistories");

            migrationBuilder.DropColumn(
                name: "OldDescriptionValue",
                table: "BugsHistories");

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "BugsHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldValue",
                table: "BugsHistories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

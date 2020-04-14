namespace BugTracker.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class BugHistoryChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldName",
                table: "BugsHistories");

            migrationBuilder.DropColumn(
                name: "NewDescriptionValue",
                table: "BugsHistories");

            migrationBuilder.DropColumn(
                name: "OldDescriptionValue",
                table: "BugsHistories");

            migrationBuilder.AddColumn<string>(
                name: "ChangedValueName",
                table: "BugsHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "BugsHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldValue",
                table: "BugsHistories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedValueName",
                table: "BugsHistories");

            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "BugsHistories");

            migrationBuilder.DropColumn(
                name: "OldValue",
                table: "BugsHistories");

            migrationBuilder.AddColumn<string>(
                name: "FieldName",
                table: "BugsHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewDescriptionValue",
                table: "BugsHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldDescriptionValue",
                table: "BugsHistories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

namespace BugTracker.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DeletedUnusedPostsFromNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Projects_ProjectId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_ProjectId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "News");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "News",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_ProjectId",
                table: "News",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Projects_ProjectId",
                table: "News",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

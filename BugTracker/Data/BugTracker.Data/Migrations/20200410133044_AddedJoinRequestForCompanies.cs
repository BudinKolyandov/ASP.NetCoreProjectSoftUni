namespace BugTracker.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedJoinRequestForCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JoinsRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    AdminId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinsRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JoinsRequests_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JoinsRequests_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JoinsRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JoinsRequests_AdminId",
                table: "JoinsRequests",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinsRequests_CompanyId",
                table: "JoinsRequests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinsRequests_UserId",
                table: "JoinsRequests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinsRequests");
        }
    }
}

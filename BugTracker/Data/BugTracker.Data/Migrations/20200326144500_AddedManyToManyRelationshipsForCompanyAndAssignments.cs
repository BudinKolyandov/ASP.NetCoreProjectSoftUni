namespace BugTracker.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedManyToManyRelationshipsForCompanyAndAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_AssigneeId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_AssigneeId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedById",
                table: "Assignments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssignmentsUsers",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentsUsers", x => new { x.UserId, x.AssignmentId });
                    table.ForeignKey(
                        name: "FK_AssignmentsUsers_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentsUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompaniesUsers",
                columns: table => new
                {
                    CompanyId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesUsers", x => new { x.CompanyId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CompaniesUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompaniesUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AdminId",
                table: "Projects",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AdminId",
                table: "Companies",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssignedById",
                table: "Assignments",
                column: "AssignedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentsUsers_AssignmentId",
                table: "AssignmentsUsers",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesUsers_UserId",
                table: "CompaniesUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_AssignedById",
                table: "Assignments",
                column: "AssignedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_AdminId",
                table: "Companies",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AdminId",
                table: "Projects",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_AssignedById",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_AdminId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AdminId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "AssignmentsUsers");

            migrationBuilder.DropTable(
                name: "CompaniesUsers");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AdminId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AdminId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_AssignedById",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AssignedById",
                table: "Assignments");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeId",
                table: "Assignments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssigneeId",
                table: "Assignments",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_AssigneeId",
                table: "Assignments",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

namespace BugTracker.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CascadeDeleteMappingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsUsers_Assignments_AssignmentId",
                table: "AssignmentsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsUsers_AspNetUsers_UserId",
                table: "AssignmentsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesUsers_Companies_CompanyId",
                table: "CompaniesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesUsers_AspNetUsers_UserId",
                table: "CompaniesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsers_Projects_ProjectId",
                table: "ProjectsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsers_AspNetUsers_UserId",
                table: "ProjectsUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsUsers_Assignments_AssignmentId",
                table: "AssignmentsUsers",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsUsers_AspNetUsers_UserId",
                table: "AssignmentsUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesUsers_Companies_CompanyId",
                table: "CompaniesUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesUsers_AspNetUsers_UserId",
                table: "CompaniesUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsers_Projects_ProjectId",
                table: "ProjectsUsers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsers_AspNetUsers_UserId",
                table: "ProjectsUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsUsers_Assignments_AssignmentId",
                table: "AssignmentsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsUsers_AspNetUsers_UserId",
                table: "AssignmentsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesUsers_Companies_CompanyId",
                table: "CompaniesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesUsers_AspNetUsers_UserId",
                table: "CompaniesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsers_Projects_ProjectId",
                table: "ProjectsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsers_AspNetUsers_UserId",
                table: "ProjectsUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsUsers_Assignments_AssignmentId",
                table: "AssignmentsUsers",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsUsers_AspNetUsers_UserId",
                table: "AssignmentsUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesUsers_Companies_CompanyId",
                table: "CompaniesUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesUsers_AspNetUsers_UserId",
                table: "CompaniesUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsers_Projects_ProjectId",
                table: "ProjectsUsers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsers_AspNetUsers_UserId",
                table: "ProjectsUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

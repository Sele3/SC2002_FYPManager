using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYPManager.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleChangeRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TitleChangeRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    RequestByStudentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestToSupervisorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleChangeRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_TitleChangeRequests_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TitleChangeRequests_Students_RequestByStudentID",
                        column: x => x.RequestByStudentID,
                        principalTable: "Students",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TitleChangeRequests_Supervisors_RequestToSupervisorID",
                        column: x => x.RequestToSupervisorID,
                        principalTable: "Supervisors",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TitleChangeRequests_ProjectID",
                table: "TitleChangeRequests",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TitleChangeRequests_RequestByStudentID",
                table: "TitleChangeRequests",
                column: "RequestByStudentID");

            migrationBuilder.CreateIndex(
                name: "IX_TitleChangeRequests_RequestToSupervisorID",
                table: "TitleChangeRequests",
                column: "RequestToSupervisorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TitleChangeRequests");
        }
    }
}

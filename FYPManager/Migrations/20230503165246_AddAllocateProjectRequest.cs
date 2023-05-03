using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYPManager.Migrations
{
    /// <inheritdoc />
    public partial class AddAllocateProjectRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllocateProjectRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    AllocateToStudentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocateProjectRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_AllocateProjectRequests_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocateProjectRequests_Students_AllocateToStudentID",
                        column: x => x.AllocateToStudentID,
                        principalTable: "Students",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllocateProjectRequests_AllocateToStudentID",
                table: "AllocateProjectRequests",
                column: "AllocateToStudentID");

            migrationBuilder.CreateIndex(
                name: "IX_AllocateProjectRequests_ProjectID",
                table: "AllocateProjectRequests",
                column: "ProjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocateProjectRequests");
        }
    }
}

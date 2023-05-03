using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYPManager.Migrations
{
    /// <inheritdoc />
    public partial class AddTransferStudentRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferStudentRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferFromSupervisorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransferToSupervisorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    RequestAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferStudentRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_TransferStudentRequests_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferStudentRequests_Supervisors_TransferFromSupervisorID",
                        column: x => x.TransferFromSupervisorID,
                        principalTable: "Supervisors",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferStudentRequests_Supervisors_TransferToSupervisorID",
                        column: x => x.TransferToSupervisorID,
                        principalTable: "Supervisors",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferStudentRequests_ProjectID",
                table: "TransferStudentRequests",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferStudentRequests_TransferFromSupervisorID",
                table: "TransferStudentRequests",
                column: "TransferFromSupervisorID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferStudentRequests_TransferToSupervisorID",
                table: "TransferStudentRequests",
                column: "TransferToSupervisorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferStudentRequests");
        }
    }
}

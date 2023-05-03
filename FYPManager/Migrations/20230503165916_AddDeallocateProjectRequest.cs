using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYPManager.Migrations
{
    /// <inheritdoc />
    public partial class AddDeallocateProjectRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeallocateProjectRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeallocateStudentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeallocateProjectRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_DeallocateProjectRequests_Students_DeallocateStudentID",
                        column: x => x.DeallocateStudentID,
                        principalTable: "Students",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeallocateProjectRequests_DeallocateStudentID",
                table: "DeallocateProjectRequests",
                column: "DeallocateStudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeallocateProjectRequests");
        }
    }
}

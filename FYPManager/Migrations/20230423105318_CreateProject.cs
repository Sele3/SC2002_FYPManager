using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYPManager.Migrations
{
    /// <inheritdoc />
    public partial class CreateProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupervisorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Projects_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Supervisors_SupervisorID",
                        column: x => x.SupervisorID,
                        principalTable: "Supervisors",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StudentID",
                table: "Projects",
                column: "StudentID",
                unique: true,
                filter: "[StudentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SupervisorID",
                table: "Projects",
                column: "SupervisorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}

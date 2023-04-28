using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYPManager.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCoordinatorToInheritSupervisor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coordinators");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Supervisors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Supervisors");

            migrationBuilder.CreateTable(
                name: "Coordinators",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinators", x => x.UserID);
                });
        }
    }
}

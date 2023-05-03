using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYPManager.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeallocatedToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeallocated",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeallocated",
                table: "Students");
        }
    }
}

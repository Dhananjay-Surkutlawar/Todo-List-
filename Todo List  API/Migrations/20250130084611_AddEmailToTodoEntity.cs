using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo_List__API.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToTodoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Todos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Todos");
        }
    }
}

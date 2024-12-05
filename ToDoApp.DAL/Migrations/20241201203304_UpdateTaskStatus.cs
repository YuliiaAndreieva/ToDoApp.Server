using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "UserTasks");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserTasks");

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "UserTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

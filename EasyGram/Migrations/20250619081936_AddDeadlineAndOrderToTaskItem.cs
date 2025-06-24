using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyGram.Migrations
{
    /// <inheritdoc />
    public partial class AddDeadlineAndOrderToTaskItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TaskItems",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "TaskItems");
        }
    }
}

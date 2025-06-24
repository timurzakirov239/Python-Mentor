using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyGram.Migrations
{
    /// <inheritdoc />
    public partial class FixModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTest_TaskItems_TaskItemId",
                table: "TaskTest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskTest",
                table: "TaskTest");

            migrationBuilder.RenameTable(
                name: "TaskTest",
                newName: "TaskTests");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTest_TaskItemId",
                table: "TaskTests",
                newName: "IX_TaskTests_TaskItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskTests",
                table: "TaskTests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTests_TaskItems_TaskItemId",
                table: "TaskTests",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTests_TaskItems_TaskItemId",
                table: "TaskTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskTests",
                table: "TaskTests");

            migrationBuilder.RenameTable(
                name: "TaskTests",
                newName: "TaskTest");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTests_TaskItemId",
                table: "TaskTest",
                newName: "IX_TaskTest_TaskItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskTest",
                table: "TaskTest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTest_TaskItems_TaskItemId",
                table: "TaskTest",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

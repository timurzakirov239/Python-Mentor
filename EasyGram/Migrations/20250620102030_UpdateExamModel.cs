using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyGram.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExamModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CorrectAnswers",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "ExamResults");

            migrationBuilder.RenameColumn(
                name: "TotalQuestions",
                table: "ExamResults",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "IsCertificateIssued",
                table: "ExamResults",
                newName: "IsPassed");

            migrationBuilder.RenameColumn(
                name: "ExamDate",
                table: "ExamResults",
                newName: "SubmittedAt");

            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "ExamResults",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults");

            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                table: "ExamResults",
                newName: "ExamDate");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "ExamResults",
                newName: "TotalQuestions");

            migrationBuilder.RenameColumn(
                name: "IsPassed",
                table: "ExamResults",
                newName: "IsCertificateIssued");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Exams",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Exams",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "ExamResults",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswers",
                table: "ExamResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "ExamResults",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "ExamResults",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");
        }
    }
}

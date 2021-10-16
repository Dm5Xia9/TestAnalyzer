using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using ViewAnalyzer.Models;

namespace ViewAnalyzer.Models.Migrations
{
    public partial class result : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerResults_Studies_StudyId",
                table: "AnalyzerResults");

            migrationBuilder.DropIndex(
                name: "IX_AnalyzerResults_StudyId",
                table: "AnalyzerResults");

            migrationBuilder.AlterColumn<List<StudyResult>>(
                name: "Result",
                table: "AnalyzerResults",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "AnalyzerResults",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patient",
                table: "AnalyzerResults",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "AnalyzerResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResultState",
                table: "AnalyzerResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnalyzerResultStudy",
                columns: table => new
                {
                    AnalyzerResultsId = table.Column<long>(type: "bigint", nullable: false),
                    StudiesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzerResultStudy", x => new { x.AnalyzerResultsId, x.StudiesId });
                    table.ForeignKey(
                        name: "FK_AnalyzerResultStudy_AnalyzerResults_AnalyzerResultsId",
                        column: x => x.AnalyzerResultsId,
                        principalTable: "AnalyzerResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalyzerResultStudy_Studies_StudiesId",
                        column: x => x.StudiesId,
                        principalTable: "Studies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerResultStudy_StudiesId",
                table: "AnalyzerResultStudy",
                column: "StudiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyzerResultStudy");

            migrationBuilder.DropColumn(
                name: "Error",
                table: "AnalyzerResults");

            migrationBuilder.DropColumn(
                name: "Patient",
                table: "AnalyzerResults");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "AnalyzerResults");

            migrationBuilder.DropColumn(
                name: "ResultState",
                table: "AnalyzerResults");

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "AnalyzerResults",
                type: "text",
                nullable: true,
                oldClrType: typeof(List<StudyResult>),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerResults_StudyId",
                table: "AnalyzerResults",
                column: "StudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerResults_Studies_StudyId",
                table: "AnalyzerResults",
                column: "StudyId",
                principalTable: "Studies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

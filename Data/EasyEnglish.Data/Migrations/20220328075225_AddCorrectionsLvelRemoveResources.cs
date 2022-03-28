using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyEnglish.Data.Migrations
{
    public partial class AddCorrectionsLvelRemoveResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Languages_LanguageId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Levels_LevelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Levels_LevelId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_LevelId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LanguageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LevelId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Resources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_LevelId",
                table: "Resources",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LanguageId",
                table: "Courses",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LevelId",
                table: "Courses",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Languages_LanguageId",
                table: "Courses",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Levels_LevelId",
                table: "Courses",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Levels_LevelId",
                table: "Resources",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");
        }
    }
}

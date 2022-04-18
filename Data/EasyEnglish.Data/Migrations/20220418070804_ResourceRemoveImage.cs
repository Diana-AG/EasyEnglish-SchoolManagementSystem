#nullable disable

namespace EasyEnglish.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ResourceRemoveImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Resources_ResourceId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ResourceId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Resources");

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ResourceId",
                table: "Images",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Resources_ResourceId",
                table: "Images",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id");
        }
    }
}

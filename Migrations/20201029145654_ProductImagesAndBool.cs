using Microsoft.EntityFrameworkCore.Migrations;

namespace MuscleStore.Migrations
{
    public partial class ProductImagesAndBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitsInStock",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "BackImageUrl",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrontImageUrl",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Product",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackImageUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FrontImageUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "UnitsInStock",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

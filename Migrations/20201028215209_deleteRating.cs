using Microsoft.EntityFrameworkCore.Migrations;

namespace MuscleStore.Migrations
{
    public partial class deleteRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "NumOfItems",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumOfItems",
                table: "Order",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfItems",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "NumOfItems",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

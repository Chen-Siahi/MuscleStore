using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MuscleStore.Migrations
{
    public partial class AccountModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SentById",
                table: "Review",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalPurchases",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitsInStock",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    StreetAddress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    CreditCardNum = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_SentById",
                table: "Review",
                column: "SentById");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Account_SentById",
                table: "Review",
                column: "SentById",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Account_SentById",
                table: "Review");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Review_SentById",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "SentById",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TotalPurchases",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UnitsInStock",
                table: "Product");
        }
    }
}

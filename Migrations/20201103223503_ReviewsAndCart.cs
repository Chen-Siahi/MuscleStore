using Microsoft.EntityFrameworkCore.Migrations;

namespace MuscleStore.Migrations
{
    public partial class ReviewsAndCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Account_AssociatedAccountId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "AddressBook");

            migrationBuilder.DropColumn(
                name: "NumOfItems",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "CreditCardNum",
                table: "Account");

            migrationBuilder.AlterColumn<int>(
                name: "AssociatedAccountId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Account_AssociatedAccountId",
                table: "Order",
                column: "AssociatedAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Account_AssociatedAccountId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "NumOfItems",
                table: "ShoppingCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ShoppingCart",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "AssociatedAccountId",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreditCardNum",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddressBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressBook_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressBook_AccountId",
                table: "AddressBook",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Account_AssociatedAccountId",
                table: "Order",
                column: "AssociatedAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

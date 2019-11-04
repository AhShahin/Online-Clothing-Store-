using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Migrations
{
    public partial class TransferingColumnsfromItemstabletoItemDetailstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserItem");

            migrationBuilder.DropColumn(
                name: "Expires",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "postcode",
                table: "Addresses",
                newName: "Postcode");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Item_Details",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Viewed",
                table: "Item_Details",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Item_Details");

            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "Item_Details");

            migrationBuilder.RenameColumn(
                name: "Postcode",
                table: "Addresses",
                newName: "postcode");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Expires",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.GRRInnovations.SmartInventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatenametable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SupplierModel_SupplierUid",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierModel",
                table: "SupplierModel");

            migrationBuilder.RenameTable(
                name: "SupplierModel",
                newName: "Suppliers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierUid",
                table: "Products",
                column: "SupplierUid",
                principalTable: "Suppliers",
                principalColumn: "Uid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierUid",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "SupplierModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierModel",
                table: "SupplierModel",
                column: "Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SupplierModel_SupplierUid",
                table: "Products",
                column: "SupplierUid",
                principalTable: "SupplierModel",
                principalColumn: "Uid");
        }
    }
}

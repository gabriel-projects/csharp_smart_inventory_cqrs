using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.GRRInnovations.SmartInventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addsupplierandstock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SupplierUid",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockEntries",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockEntries", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_StockEntries_Products_ProductUid",
                        column: x => x.ProductUid,
                        principalTable: "Products",
                        principalColumn: "Uid");
                });

            migrationBuilder.CreateTable(
                name: "StockOutputs",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutputDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOutputs", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_StockOutputs_Products_ProductUid",
                        column: x => x.ProductUid,
                        principalTable: "Products",
                        principalColumn: "Uid");
                });

            migrationBuilder.CreateTable(
                name: "SupplierModel",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierModel", x => x.Uid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierUid",
                table: "Products",
                column: "SupplierUid");

            migrationBuilder.CreateIndex(
                name: "IX_StockEntries_ProductUid",
                table: "StockEntries",
                column: "ProductUid");

            migrationBuilder.CreateIndex(
                name: "IX_StockOutputs_ProductUid",
                table: "StockOutputs",
                column: "ProductUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SupplierModel_SupplierUid",
                table: "Products",
                column: "SupplierUid",
                principalTable: "SupplierModel",
                principalColumn: "Uid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SupplierModel_SupplierUid",
                table: "Products");

            migrationBuilder.DropTable(
                name: "StockEntries");

            migrationBuilder.DropTable(
                name: "StockOutputs");

            migrationBuilder.DropTable(
                name: "SupplierModel");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplierUid",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplierUid",
                table: "Products");
        }
    }
}

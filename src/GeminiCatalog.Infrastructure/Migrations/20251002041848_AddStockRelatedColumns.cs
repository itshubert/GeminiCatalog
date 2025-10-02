using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeminiCatalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockRelatedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableStock",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastStockUpdate",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockStatus",
                table: "Products",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "OutOfStock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableStock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastStockUpdate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockStatus",
                table: "Products");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce_System.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitPriceToOrderProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "unitPrice",
                table: "OrderProducts",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unitPrice",
                table: "OrderProducts");
        }
    }
}

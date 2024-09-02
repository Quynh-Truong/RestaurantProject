using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "Index_TableNo",
                table: "Tables",
                column: "TableNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index_PhoneNo",
                table: "Customers",
                column: "PhoneNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_TableNo",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "Index_PhoneNo",
                table: "Customers");
        }
    }
}

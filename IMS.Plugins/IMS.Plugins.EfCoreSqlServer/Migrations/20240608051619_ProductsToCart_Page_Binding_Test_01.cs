using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Plugins.EfCoreSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class ProductsToCart_Page_Binding_Test_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchQty",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchQty",
                table: "Products");
        }
    }
}

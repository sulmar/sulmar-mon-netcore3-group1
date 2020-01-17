using Microsoft.EntityFrameworkCore.Migrations;

namespace Sulmar.Shopping.Infrastructure.Migrations
{
    public partial class AddHashPasswordToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashPassword",
                table: "Customers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "Customers");
        }
    }
}

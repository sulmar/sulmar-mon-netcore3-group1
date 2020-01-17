using Microsoft.EntityFrameworkCore.Migrations;

namespace Sulmar.Shopping.Infrastructure.Migrations
{
    public partial class AddGenderToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Customers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Sulmar.Shopping.Infrastructure.Migrations
{
    public partial class AddLoginToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE dbo.Customers SET Login = FirstName WHERE Login=''");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Customers");
        }
    }
}

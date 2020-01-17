using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sulmar.Shopping.Infrastructure.Migrations
{
    public partial class AddVersionToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Customers",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Customers");
        }
    }
}

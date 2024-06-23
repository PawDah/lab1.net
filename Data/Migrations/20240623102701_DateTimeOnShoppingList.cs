using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab1.net.Data.Migrations
{
    /// <inheritdoc />
    public partial class DateTimeOnShoppingList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Planned",
                table: "ShoppingList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Planned",
                table: "ShoppingList");
        }
    }
}

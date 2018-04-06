using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class AddFieldStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "CustomFields");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CustomFields",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CustomFields");

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "CustomFields",
                nullable: false,
                defaultValue: false);
        }
    }
}

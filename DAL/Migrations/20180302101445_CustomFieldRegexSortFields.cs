using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class CustomFieldRegexSortFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegexPattern",
                table: "CustomFields",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "CustomFieldInTasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegexPattern",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "CustomFieldInTasks");
        }
    }
}

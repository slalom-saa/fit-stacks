using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Slalom.Stacks.ConsoleClient.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "Audits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Audits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThreadId",
                table: "Audits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserHostAddress",
                table: "Audits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Host",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "UserHostAddress",
                table: "Audits");
        }
    }
}

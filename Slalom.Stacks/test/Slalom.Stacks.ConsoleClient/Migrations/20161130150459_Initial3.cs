using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Slalom.Stacks.ConsoleClient.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThreadId",
                table: "Logs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserHostAddress",
                table: "Logs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Host",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "UserHostAddress",
                table: "Logs");
        }
    }
}

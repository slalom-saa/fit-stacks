using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Slalom.Stacks.ConsoleClient.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationName = table.Column<string>(nullable: true),
                    CorrelationId = table.Column<Guid>(nullable: false),
                    Environment = table.Column<string>(nullable: true),
                    EventId = table.Column<Guid>(nullable: true),
                    EventName = table.Column<string>(nullable: true),
                    MachineName = table.Column<string>(nullable: true),
                    Payload = table.Column<string>(nullable: true),
                    SessionId = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationName = table.Column<string>(nullable: true),
                    CommandId = table.Column<Guid>(nullable: false),
                    CommandName = table.Column<string>(nullable: true),
                    CorrelationId = table.Column<Guid>(nullable: false),
                    Environment = table.Column<string>(nullable: true),
                    IsSuccessful = table.Column<bool>(nullable: false),
                    MachineName = table.Column<string>(nullable: true),
                    Payload = table.Column<string>(nullable: true),
                    SessionId = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    ValidationErrors = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}

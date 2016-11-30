using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Slalom.Stacks.ConsoleClient.Migrations
{
    public partial class Another : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationName = table.Column<string>(nullable: true),
                    CommandId = table.Column<Guid>(nullable: false),
                    CommandName = table.Column<string>(nullable: true),
                    CommandPayload = table.Column<string>(nullable: true),
                    CommandTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    CorrelationId = table.Column<Guid>(nullable: false),
                    Environment = table.Column<string>(nullable: true),
                    EventId = table.Column<Guid>(nullable: true),
                    EventName = table.Column<string>(nullable: true),
                    EventPayload = table.Column<string>(nullable: true),
                    EventTimeStamp = table.Column<DateTimeOffset>(nullable: true),
                    IsSuccessful = table.Column<bool>(nullable: false),
                    MachineName = table.Column<string>(nullable: true),
                    SessionId = table.Column<string>(nullable: true),
                    StateChanged = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");
        }
    }
}

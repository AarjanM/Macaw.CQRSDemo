using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Macaw.CQRSDemo.Infrastructure.Repositories.Migrations
{
    public partial class FirstModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventType = table.Column<string>(nullable: true),
                    PartitionKey = table.Column<string>(nullable: true),
                    Payload = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<string>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    Score1 = table.Column<int>(nullable: false),
                    Score2 = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Team1 = table.Column<string>(nullable: true),
                    Team2 = table.Column<string>(nullable: true),
                    Timeouts1 = table.Column<string>(nullable: true),
                    Timeouts2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}

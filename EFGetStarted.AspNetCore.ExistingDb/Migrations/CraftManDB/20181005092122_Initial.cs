using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFGetStarted.AspNetCore.ExistingDb.Migrations.CraftManDB
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Håndværker",
                columns: table => new
                {
                    HåndværkerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ansættelsedato = table.Column<DateTime>(type: "date", nullable: false),
                    Efternavn = table.Column<string>(maxLength: 50, nullable: false),
                    Fagområde = table.Column<string>(maxLength: 50, nullable: true),
                    Fornavn = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Håndværker", x => x.HåndværkerId);
                });

            migrationBuilder.CreateTable(
                name: "Værktøjskasse",
                columns: table => new
                {
                    VKasseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Anskaffet = table.Column<DateTime>(type: "date", nullable: false),
                    Fabrikat = table.Column<string>(maxLength: 50, nullable: true),
                    EjesAf = table.Column<int>(nullable: true),
                    Model = table.Column<string>(maxLength: 50, nullable: true),
                    Serienummer = table.Column<string>(maxLength: 50, nullable: true),
                    Farve = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Værktøjskasse", x => x.VKasseId);
                    table.ForeignKey(
                        name: "FK_Værktøjskasse_ToHåndværker",
                        column: x => x.EjesAf,
                        principalTable: "Håndværker",
                        principalColumn: "HåndværkerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Værktøj",
                columns: table => new
                {
                    VærktøjsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Anskaffet = table.Column<DateTime>(type: "date", nullable: false),
                    Fabrikat = table.Column<string>(nullable: true),
                    Model = table.Column<string>(maxLength: 50, nullable: true),
                    Serienr = table.Column<string>(maxLength: 50, nullable: true),
                    Type = table.Column<string>(maxLength: 50, nullable: true),
                    LiggerIVTK = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Værktøj", x => x.VærktøjsId);
                    table.ForeignKey(
                        name: "FK_Værktøj_Værktøjskasse",
                        column: x => x.LiggerIVTK,
                        principalTable: "Værktøjskasse",
                        principalColumn: "VKasseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Værktøj_LiggerIVTK",
                table: "Værktøj",
                column: "LiggerIVTK");

            migrationBuilder.CreateIndex(
                name: "IX_Værktøjskasse_EjesAf",
                table: "Værktøjskasse",
                column: "EjesAf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Værktøj");

            migrationBuilder.DropTable(
                name: "Værktøjskasse");

            migrationBuilder.DropTable(
                name: "Håndværker");
        }
    }
}

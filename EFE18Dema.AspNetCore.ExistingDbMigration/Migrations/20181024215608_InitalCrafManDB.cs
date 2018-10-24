using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFE18Demo.AspNetCore.ExistingDbMigration.Migrations
{
    public partial class InitalCrafManDB : Migration
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
                    VTKAnskaffet = table.Column<DateTime>(type: "date", nullable: false),
                    VTKFabrikat = table.Column<string>(maxLength: 50, nullable: true),
                    HåndværkerID = table.Column<int>(nullable: true),
                    VTKModel = table.Column<string>(maxLength: 50, nullable: true),
                    VTKSerienummer = table.Column<string>(maxLength: 50, nullable: true),
                    VTKFarve = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Værktøjskasse", x => x.VKasseId);
                    table.ForeignKey(
                        name: "FK_Værktøjskasse_ToHåndværker",
                        column: x => x.HåndværkerID,
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
                    VTAnskaffet = table.Column<DateTime>(type: "date", nullable: false),
                    VTFabrikat = table.Column<string>(nullable: true),
                    VTModel = table.Column<string>(maxLength: 50, nullable: true),
                    VTSerienr = table.Column<string>(maxLength: 50, nullable: true),
                    VTType = table.Column<string>(maxLength: 50, nullable: true),
                    VTKId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Værktøj", x => x.VærktøjsId);
                    table.ForeignKey(
                        name: "FK_Værktøj_Værktøjskasse",
                        column: x => x.VTKId,
                        principalTable: "Værktøjskasse",
                        principalColumn: "VKasseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Værktøj_VTKId",
                table: "Værktøj",
                column: "VTKId");

            migrationBuilder.CreateIndex(
                name: "IX_Værktøjskasse_HåndværkerID",
                table: "Værktøjskasse",
                column: "HåndværkerID");
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

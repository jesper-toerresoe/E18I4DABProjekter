using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFE18Demo4.AspNetCore.ExistingDbMigration.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Haandvaerker",
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
                    table.PrimaryKey("PK_Haandvaerker", x => x.HåndværkerId);
                });

            migrationBuilder.CreateTable(
                name: "Vaerktoejskasse",
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
                    table.PrimaryKey("PK_Vaerktoejskasse", x => x.VKasseId);
                    table.ForeignKey(
                        name: "FK_Værktøjskasse_ToHåndværker",
                        column: x => x.HåndværkerID,
                        principalTable: "Haandvaerker",
                        principalColumn: "HåndværkerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaerktoej",
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
                    table.PrimaryKey("PK_Vaerktoej", x => x.VærktøjsId);
                    table.ForeignKey(
                        name: "FK_Værktøj_Værktøjskasse",
                        column: x => x.VTKId,
                        principalTable: "Vaerktoejskasse",
                        principalColumn: "VKasseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaerktoej_VTKId",
                table: "Vaerktoej",
                column: "VTKId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaerktoejskasse_HåndværkerID",
                table: "Vaerktoejskasse",
                column: "HåndværkerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vaerktoej");

            migrationBuilder.DropTable(
                name: "Vaerktoejskasse");

            migrationBuilder.DropTable(
                name: "Haandvaerker");
        }
    }
}

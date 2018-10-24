using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFGetStarted.AspNetCore.ExistingDbMigration.Migrations
{
    public partial class InitialRemoveDKNamingHVVTKVT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Haandvaerker",
                columns: table => new
                {
                    HaandvaerkerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HVAnsaettelsedato = table.Column<DateTime>(type: "date", nullable: false),
                    HVEfternavn = table.Column<string>(maxLength: 50, nullable: false),
                    HVFagomraade = table.Column<string>(maxLength: 50, nullable: true),
                    HVFornavn = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Haandvaerker", x => x.HaandvaerkerId);
                });

            migrationBuilder.CreateTable(
                name: "Vaerktoejskasse",
                columns: table => new
                {
                    VKasseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VTKAnskaffet = table.Column<DateTime>(type: "date", nullable: false),
                    VTKFabrikat = table.Column<string>(maxLength: 50, nullable: true),
                    VTKEjesAf = table.Column<int>(nullable: true),
                    VTKModel = table.Column<string>(maxLength: 50, nullable: true),
                    VTKSerienummer = table.Column<string>(maxLength: 50, nullable: true),
                    VTKFarve = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaerktoejskasse", x => x.VKasseId);
                    table.ForeignKey(
                        name: "FK_Vaerktoejskasse_ToHaandvaerker",
                        column: x => x.VTKEjesAf,
                        principalTable: "Haandvaerker",
                        principalColumn: "HaandvaerkerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaerktoej",
                columns: table => new
                {
                    VTId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VTAnskaffet = table.Column<DateTime>(type: "date", nullable: false),
                    VTFabrikat = table.Column<string>(nullable: true),
                    VTModel = table.Column<string>(maxLength: 50, nullable: true),
                    VTSerienr = table.Column<string>(maxLength: 50, nullable: true),
                    VTType = table.Column<string>(maxLength: 50, nullable: true),
                    LiggerIVTK = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaerktoej", x => x.VTId);
                    table.ForeignKey(
                        name: "FK_Vaerktoej_Vaerktsejskasse",
                        column: x => x.LiggerIVTK,
                        principalTable: "Vaerktoejskasse",
                        principalColumn: "VKasseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaerktoej_LiggerIVTK",
                table: "Vaerktoej",
                column: "LiggerIVTK");

            migrationBuilder.CreateIndex(
                name: "IX_Vaerktoejskasse_VTKEjesAf",
                table: "Vaerktoejskasse",
                column: "VTKEjesAf");
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

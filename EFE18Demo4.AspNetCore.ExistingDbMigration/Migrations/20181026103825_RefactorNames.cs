using Microsoft.EntityFrameworkCore.Migrations;

namespace EFE18Demo4.AspNetCore.ExistingDbMigration.Migrations
{
    public partial class RefactorNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VærktøjsId",
                table: "Vaerktoej",
                newName: "VaerktoejsId");

            migrationBuilder.RenameColumn(
                name: "Fagområde",
                table: "Haandvaerker",
                newName: "Fagomraade");

            migrationBuilder.RenameColumn(
                name: "Ansættelsedato",
                table: "Haandvaerker",
                newName: "Ansaettelsedato");

            migrationBuilder.RenameColumn(
                name: "HåndværkerId",
                table: "Haandvaerker",
                newName: "HaandvaerkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VaerktoejsId",
                table: "Vaerktoej",
                newName: "VærktøjsId");

            migrationBuilder.RenameColumn(
                name: "Fagomraade",
                table: "Haandvaerker",
                newName: "Fagområde");

            migrationBuilder.RenameColumn(
                name: "Ansaettelsedato",
                table: "Haandvaerker",
                newName: "Ansættelsedato");

            migrationBuilder.RenameColumn(
                name: "HaandvaerkerId",
                table: "Haandvaerker",
                newName: "HåndværkerId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LZMotel.Local.API.Migrations
{
    public partial class LocalSuite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suite",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SuiteNumero = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 4),
                    SuiteTransferida = table.Column<int>(type: "integer", nullable: false),
                    Comanda = table.Column<int>(type: "integer", nullable: false),
                    CategoriaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suite", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suite_SuiteNumero",
                table: "Suite",
                column: "SuiteNumero",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suite");
        }
    }
}

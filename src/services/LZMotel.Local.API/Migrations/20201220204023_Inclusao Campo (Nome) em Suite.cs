using Microsoft.EntityFrameworkCore.Migrations;

namespace LZMotel.Local.API.Migrations
{
    public partial class InclusaoCampoNomeemSuite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Suite",
                type: "varchar(25)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Suite");
        }
    }
}

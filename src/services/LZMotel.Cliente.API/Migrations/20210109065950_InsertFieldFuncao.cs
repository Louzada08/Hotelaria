using Microsoft.EntityFrameworkCore.Migrations;

namespace LZMotel.Clientes.API.Migrations
{
    public partial class InsertFieldFuncao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Funcao",
                table: "Cliente",
                type: "char(15)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Funcao",
                table: "Cliente");
        }
    }
}

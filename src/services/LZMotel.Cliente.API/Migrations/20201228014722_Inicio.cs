using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LZMotel.Clientes.API.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Turno = table.Column<string>(type: "char(02)", nullable: false),
                    Email = table.Column<string>(type: "varchar(120)", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Excluido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                { 
                    Id = table.Column<Guid>(nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(70)", nullable: false),
                    Numero = table.Column<string>(type: "char(10)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(50)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(70)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(20)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(50)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(20)", nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ClienteId",
                table: "Enderecos",
                column: "ClienteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}

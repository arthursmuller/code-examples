using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddEnderecoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ENDERECO_CLIENTE",
                columns: table => new
                {
                    ID_ENDERECO_CLIENTE = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    ID_CLIENTE = table.Column<int>(nullable: false),
                    TITULO = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ENDERECO = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    ID_MUNICIPIO = table.Column<int>(nullable: false),
                    BAIRRO = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    CEP = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    DELETADO = table.Column<bool>(nullable: false),
                    PRINCIPAL = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECO_CLIENTE", x => x.ID_ENDERECO_CLIENTE);
                    table.ForeignKey(
                        name: "FK_ENDERECO_CLIENTE_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE");
                    table.ForeignKey(
                        name: "FK_ENDERECO_CLIENTE_MUNICIPIO_ID_MUNICIPIO",
                        column: x => x.ID_MUNICIPIO,
                        principalTable: "MUNICIPIO",
                        principalColumn: "ID_MUNICIPIO");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ENDERECO_CLIENTE");
        }
    }
}

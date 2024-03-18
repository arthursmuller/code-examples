using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class IncluirClienteStubTelefones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID_CLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "TELEFONE_CLIENTE",
                columns: table => new
                {
                    ID_TELEFONE_CLIENTE = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    DDD = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    FONE = table.Column<string>(unicode: false, maxLength: 9, nullable: false),
                    ID_CLIENTE = table.Column<int>(nullable: false),
                    DELETADO = table.Column<bool>(nullable: false),
                    PRINCIPAL = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TELEFONE_CLIENTE", x => x.ID_TELEFONE_CLIENTE);
                    table.ForeignKey(
                        name: "FK_TELEFONE_CLIENTE_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TELEFONE_CLIENTE_ID_CLIENTE",
                table: "TELEFONE_CLIENTE",
                column: "ID_CLIENTE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TELEFONE_CLIENTE");

            migrationBuilder.DropTable(
                name: "CLIENTE");
        }
    }
}

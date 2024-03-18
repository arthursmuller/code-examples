using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddValoresMargemRendimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MARGEM_DISPONIVEL",
                table: "RENDIMENTO_CLIENTE",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MARGEM_DISPONIVEL_CARTAO",
                table: "RENDIMENTO_CLIENTE",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConsultaBeneficiosInssCliente",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPaperlessDocumento = table.Column<int>(type: "int", nullable: true),
                    IdAnexoArquivoTermo = table.Column<int>(type: "int", nullable: true),
                    ChaveAutorizacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataGeracaoArquivoTermo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteID = table.Column<int>(type: "int", nullable: true),
                    AnexoArquivoTermoID = table.Column<int>(type: "int", nullable: true),
                    UsuarioAtualizacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultaBeneficiosInssCliente", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConsultaBeneficiosInssCliente_ANEXO_AnexoArquivoTermoID",
                        column: x => x.AnexoArquivoTermoID,
                        principalTable: "ANEXO",
                        principalColumn: "ID_ANEXO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultaBeneficiosInssCliente_CLIENTE_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultaBeneficiosInssCliente_AnexoArquivoTermoID",
                table: "ConsultaBeneficiosInssCliente",
                column: "AnexoArquivoTermoID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultaBeneficiosInssCliente_ClienteID",
                table: "ConsultaBeneficiosInssCliente",
                column: "ClienteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultaBeneficiosInssCliente");

            migrationBuilder.DropColumn(
                name: "MARGEM_DISPONIVEL",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "MARGEM_DISPONIVEL_CARTAO",
                table: "RENDIMENTO_CLIENTE");
        }
    }
}

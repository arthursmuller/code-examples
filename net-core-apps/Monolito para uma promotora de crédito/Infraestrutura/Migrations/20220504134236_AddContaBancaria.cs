using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddContaBancaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdMeioPagamento",
                table: "SEGURO_PROPOSTA",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdContaBancaria",
                table: "CLIENTE",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CONTA_BANCARIA",
                columns: table => new
                {
                    ID_CONTA_BANCARIA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AGENCIA = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    NUMERO_CONTA = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DIGITO_VERIFICADOR_AGENCIA = table.Column<int>(type: "int", nullable: false),
                    IdBanco = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTA_BANCARIA", x => x.ID_CONTA_BANCARIA);
                    table.ForeignKey(
                        name: "FK_CONTA_BANCARIA_BANCO_IdBanco",
                        column: x => x.IdBanco,
                        principalTable: "BANCO",
                        principalColumn: "ID_BANCO");
                });

            migrationBuilder.CreateTable(
                name: "MEIO_PAGAMENTO_SEGURO",
                columns: table => new
                {
                    ID_MEIO_PAGAMENTO_SEGURO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    ID_MEIO_PAGAMENTO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEIO_PAGAMENTO_SEGURO", x => x.ID_MEIO_PAGAMENTO_SEGURO);
                    table.ForeignKey(
                        name: "FK_MEIO_PAGAMENTO_SEGURO_PRODUTO_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "PRODUTO",
                        principalColumn: "ID_PRODUTO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_IdContaBancaria",
                table: "CLIENTE",
                column: "IdContaBancaria");

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_BANCARIA_IdBanco",
                table: "CONTA_BANCARIA",
                column: "IdBanco");

            migrationBuilder.CreateIndex(
                name: "IX_MEIO_PAGAMENTO_SEGURO_IdProduto",
                table: "MEIO_PAGAMENTO_SEGURO",
                column: "IdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_CONTA_BANCARIA_IdContaBancaria",
                table: "CLIENTE",
                column: "IdContaBancaria",
                principalTable: "CONTA_BANCARIA",
                principalColumn: "ID_CONTA_BANCARIA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_CONTA_BANCARIA_IdContaBancaria",
                table: "CLIENTE");

            migrationBuilder.DropTable(
                name: "CONTA_BANCARIA");

            migrationBuilder.DropTable(
                name: "MEIO_PAGAMENTO_SEGURO");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_IdContaBancaria",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "IdMeioPagamento",
                table: "SEGURO_PROPOSTA");

            migrationBuilder.DropColumn(
                name: "IdContaBancaria",
                table: "CLIENTE");
        }
    }
}

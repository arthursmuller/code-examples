using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class AddNovasComunicacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DDD",
                table: "WHATSAPP_MENSAGEM");

            migrationBuilder.DropColumn(
                name: "TELEFONE",
                table: "WHATSAPP_MENSAGEM");

            migrationBuilder.AlterColumn<string>(
                name: "MENSAGEM_ENVIO",
                table: "WHATSAPP_MENSAGEM",
                type: "varchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AddColumn<string>(
                name: "CODIGO_REFERENCIA_MENSAGEM",
                table: "WHATSAPP_MENSAGEM",
                type: "varchar(13)",
                unicode: false,
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ID_WHATSAPP_FORNECEDOR",
                table: "WHATSAPP_MENSAGEM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NUMERO_TELEFONE",
                table: "WHATSAPP_MENSAGEM",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TORPEDO_VOZ_FORNECEDOR",
                columns: table => new
                {
                    ID_TORPEDO_VOZ_FORNECEDOR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME_EXIBICAO = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CHAVE_ENVIO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ID_EMPRESA = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TORPEDO_VOZ_FORNECEDOR", x => x.ID_TORPEDO_VOZ_FORNECEDOR);
                    table.ForeignKey(
                        name: "FK_TORPEDO_VOZ_FORNECEDOR_EMPRESA_ID_EMPRESA",
                        column: x => x.ID_EMPRESA,
                        principalTable: "EMPRESA",
                        principalColumn: "ID_EMPRESA");
                });

            migrationBuilder.CreateTable(
                name: "WHATSAPP_FORNECEDOR",
                columns: table => new
                {
                    ID_WHATSAPP_FORNECEDOR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME_EXIBICAO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CHAVE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ID_EMPRESA = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WHATSAPP_FORNECEDOR", x => x.ID_WHATSAPP_FORNECEDOR);
                    table.ForeignKey(
                        name: "FK_WHATSAPP_FORNECEDOR_EMPRESA_ID_EMPRESA",
                        column: x => x.ID_EMPRESA,
                        principalTable: "EMPRESA",
                        principalColumn: "ID_EMPRESA");
                });

            migrationBuilder.CreateTable(
                name: "TORPEDO_VOZ_MENSAGEM",
                columns: table => new
                {
                    ID_TORPEDO_VOZ_MENSAGEM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODIGO_REFERENCIA_MENSAGEM = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    NUMERO_TELEFONE = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    MENSAGEM = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    SITUACAO = table.Column<bool>(type: "bit", nullable: false),
                    DATA_INSERCAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_TORPEDO_VOZ_FORNECEDOR = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TORPEDO_VOZ_MENSAGEM", x => x.ID_TORPEDO_VOZ_MENSAGEM);
                    table.ForeignKey(
                        name: "FK_TORPEDO_VOZ_MENSAGEM_TORPEDO_VOZ_FORNECEDOR_ID_TORPEDO_VOZ_FORNECEDOR",
                        column: x => x.ID_TORPEDO_VOZ_FORNECEDOR,
                        principalTable: "TORPEDO_VOZ_FORNECEDOR",
                        principalColumn: "ID_TORPEDO_VOZ_FORNECEDOR");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WHATSAPP_MENSAGEM_ID_WHATSAPP_FORNECEDOR",
                table: "WHATSAPP_MENSAGEM",
                column: "ID_WHATSAPP_FORNECEDOR");

            migrationBuilder.CreateIndex(
                name: "IX_TORPEDO_VOZ_FORNECEDOR_ID_EMPRESA",
                table: "TORPEDO_VOZ_FORNECEDOR",
                column: "ID_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_TORPEDO_VOZ_MENSAGEM_ID_TORPEDO_VOZ_FORNECEDOR",
                table: "TORPEDO_VOZ_MENSAGEM",
                column: "ID_TORPEDO_VOZ_FORNECEDOR");

            migrationBuilder.CreateIndex(
                name: "IX_WHATSAPP_FORNECEDOR_ID_EMPRESA",
                table: "WHATSAPP_FORNECEDOR",
                column: "ID_EMPRESA");

            migrationBuilder.AddForeignKey(
                name: "FK_WHATSAPP_MENSAGEM_WHATSAPP_FORNECEDOR_ID_WHATSAPP_FORNECEDOR",
                table: "WHATSAPP_MENSAGEM",
                column: "ID_WHATSAPP_FORNECEDOR",
                principalTable: "WHATSAPP_FORNECEDOR",
                principalColumn: "ID_WHATSAPP_FORNECEDOR");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WHATSAPP_MENSAGEM_WHATSAPP_FORNECEDOR_ID_WHATSAPP_FORNECEDOR",
                table: "WHATSAPP_MENSAGEM");

            migrationBuilder.DropTable(
                name: "TORPEDO_VOZ_MENSAGEM");

            migrationBuilder.DropTable(
                name: "WHATSAPP_FORNECEDOR");

            migrationBuilder.DropTable(
                name: "TORPEDO_VOZ_FORNECEDOR");

            migrationBuilder.DropIndex(
                name: "IX_WHATSAPP_MENSAGEM_ID_WHATSAPP_FORNECEDOR",
                table: "WHATSAPP_MENSAGEM");

            migrationBuilder.DropColumn(
                name: "CODIGO_REFERENCIA_MENSAGEM",
                table: "WHATSAPP_MENSAGEM");

            migrationBuilder.DropColumn(
                name: "ID_WHATSAPP_FORNECEDOR",
                table: "WHATSAPP_MENSAGEM");

            migrationBuilder.DropColumn(
                name: "NUMERO_TELEFONE",
                table: "WHATSAPP_MENSAGEM");

            migrationBuilder.AlterColumn<string>(
                name: "MENSAGEM_ENVIO",
                table: "WHATSAPP_MENSAGEM",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)");

            migrationBuilder.AddColumn<string>(
                name: "DDD",
                table: "WHATSAPP_MENSAGEM",
                type: "varchar(3)",
                unicode: false,
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TELEFONE",
                table: "WHATSAPP_MENSAGEM",
                type: "varchar(9)",
                unicode: false,
                maxLength: 9,
                nullable: false,
                defaultValue: "");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "USUARIOS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "USUARIOS",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "TIPOS_OPERACAO",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "TIPOS_OPERACAO",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "TELEFONES_LOJAS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "TELEFONES_LOJAS",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "SITUACOES_INTENCAO_OPERACAO",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "SITUACOES_INTENCAO_OPERACAO",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "PRODUTOS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "PRODUTOS",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "PARAMETROS_OPERACAO",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "PARAMETROS_OPERACAO",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "LOJAS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "LOJAS",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "LEADS",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "INTENCOES_OPERACAO",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "CONVENIOS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "CONVENIOS",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "ANEXOS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "ANEXOS",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TiposConta",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioAtualizacao = table.Column<string>(nullable: true),
                    DataAtualizacao = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sigla = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposConta", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TiposVinculoInsticional",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioAtualizacao = table.Column<string>(nullable: true),
                    DataAtualizacao = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVinculoInsticional", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiposConta");

            migrationBuilder.DropTable(
                name: "TiposVinculoInsticional");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "USUARIOS");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "USUARIOS");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "TIPOS_OPERACAO");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "TIPOS_OPERACAO");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "TELEFONES_LOJAS");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "TELEFONES_LOJAS");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "SITUACOES_INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "SITUACOES_INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "PRODUTOS");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "PRODUTOS");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "LOJAS");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "LOJAS");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "LEADS");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "CONVENIOS");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "CONVENIOS");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "ANEXOS");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "ANEXOS");
        }
    }
}

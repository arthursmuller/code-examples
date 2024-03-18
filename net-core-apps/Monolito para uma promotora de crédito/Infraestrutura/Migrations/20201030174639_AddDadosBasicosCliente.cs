using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddDadosBasicosCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_NASCIMENTO",
                table: "CLIENTE",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "DEFICIENTE_VISUAL",
                table: "CLIENTE",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EMAIL",
                table: "CLIENTE",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FILIACAO1",
                table: "CLIENTE",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FILIACAO2",
                table: "CLIENTE",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_CIDADE_NATAL",
                table: "CLIENTE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_ESTADO_CIVIL",
                table: "CLIENTE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_GENERO",
                table: "CLIENTE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_GRAU_INSTRUCAO",
                table: "CLIENTE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "CLIENTE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NOME",
                table: "CLIENTE",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_CIDADE_NATAL",
                table: "CLIENTE",
                column: "ID_CIDADE_NATAL");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_ESTADO_CIVIL",
                table: "CLIENTE",
                column: "ID_ESTADO_CIVIL");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_GENERO",
                table: "CLIENTE",
                column: "ID_GENERO");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_GRAU_INSTRUCAO",
                table: "CLIENTE",
                column: "ID_GRAU_INSTRUCAO");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_IdUsuario",
                table: "CLIENTE",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_MUNICIPIO_ID_CIDADE_NATAL",
                table: "CLIENTE",
                column: "ID_CIDADE_NATAL",
                principalTable: "MUNICIPIO",
                principalColumn: "ID_MUNICIPIO");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_ESTADO_CIVIL_ID_ESTADO_CIVIL",
                table: "CLIENTE",
                column: "ID_ESTADO_CIVIL",
                principalTable: "ESTADO_CIVIL",
                principalColumn: "ID_ESTADO_CIVIL");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_GENERO_ID_GENERO",
                table: "CLIENTE",
                column: "ID_GENERO",
                principalTable: "GENERO",
                principalColumn: "ID_GENERO");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_GRAU_INSTRUCAO_ID_GRAU_INSTRUCAO",
                table: "CLIENTE",
                column: "ID_GRAU_INSTRUCAO",
                principalTable: "GRAU_INSTRUCAO",
                principalColumn: "ID_GRAU_INSTRUCAO");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_USUARIO_IdUsuario",
                table: "CLIENTE",
                column: "IdUsuario",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_MUNICIPIO_ID_CIDADE_NATAL",
                table: "CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_ESTADO_CIVIL_ID_ESTADO_CIVIL",
                table: "CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_GENERO_ID_GENERO",
                table: "CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_GRAU_INSTRUCAO_ID_GRAU_INSTRUCAO",
                table: "CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_USUARIO_IdUsuario",
                table: "CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_CIDADE_NATAL",
                table: "CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_ESTADO_CIVIL",
                table: "CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_GENERO",
                table: "CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_GRAU_INSTRUCAO",
                table: "CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_IdUsuario",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "DATA_NASCIMENTO",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "DEFICIENTE_VISUAL",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "EMAIL",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "FILIACAO1",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "FILIACAO2",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_CIDADE_NATAL",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_ESTADO_CIVIL",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_GENERO",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_GRAU_INSTRUCAO",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "NOME",
                table: "CLIENTE");
        }
    }
}

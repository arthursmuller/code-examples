using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AnexoDataCadastro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANEXO_USUARIO_IdUsuario",
                table: "ANEXO");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "ANEXO",
                newName: "ID_USUARIO");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_CADASTRO",
                table: "ANEXO",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ANEXO_USUARIO_ID_USUARIO",
                table: "ANEXO",
                column: "ID_USUARIO",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANEXO_USUARIO_ID_USUARIO",
                table: "ANEXO");

            migrationBuilder.DropColumn(
                name: "DATA_CADASTRO",
                table: "ANEXO");

            migrationBuilder.RenameColumn(
                name: "ID_USUARIO",
                table: "ANEXO",
                newName: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_ANEXO_USUARIO_IdUsuario",
                table: "ANEXO",
                column: "IdUsuario",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

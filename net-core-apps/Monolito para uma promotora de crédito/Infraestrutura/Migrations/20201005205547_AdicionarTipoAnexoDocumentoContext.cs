using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AdicionarTipoAnexoDocumentoContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "ANEXO");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "ANEXO",
                newName: "LINK");

            migrationBuilder.AddColumn<int>(
                name: "ID_TIPO",
                table: "ANEXO",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TIPO_DOCUMENTO",
                columns: table => new
                {
                    ID_TIPO_DOCUMENTO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    NOME = table.Column<string>(maxLength: 30, nullable: false),
                    CODIGO = table.Column<string>(maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_DOCUMENTO", x => x.ID_TIPO_DOCUMENTO);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ANEXO_ID_TIPO",
                table: "ANEXO",
                column: "ID_TIPO");

            migrationBuilder.AddForeignKey(
                name: "FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO",
                table: "ANEXO",
                column: "ID_TIPO",
                principalTable: "TIPO_DOCUMENTO",
                principalColumn: "ID_TIPO_DOCUMENTO",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO",
                table: "ANEXO");

            migrationBuilder.DropTable(
                name: "TIPO_DOCUMENTO");

            migrationBuilder.DropIndex(
                name: "IX_ANEXO_ID_TIPO",
                table: "ANEXO");

            migrationBuilder.DropColumn(
                name: "ID_TIPO",
                table: "ANEXO");

            migrationBuilder.RenameColumn(
                name: "LINK",
                table: "ANEXO",
                newName: "Link");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "ANEXO",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

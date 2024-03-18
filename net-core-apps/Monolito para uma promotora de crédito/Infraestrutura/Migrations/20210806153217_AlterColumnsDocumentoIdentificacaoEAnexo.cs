using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AlterColumnsDocumentoIdentificacaoEAnexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO",
                table: "ANEXO");

            migrationBuilder.DropIndex(
                name: "IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_NUMERO_ID_TIPO_DOCUMENTO_ID_UNIDADE_FEDERATIVA",
                table: "DOCUMENTO_IDENTIFICACAO_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "ID_TIPO",
                table: "ANEXO",
                newName: "ID_TIPO_DOCUMENTO");

            migrationBuilder.RenameIndex(
                name: "IX_ANEXO_ID_TIPO",
                table: "ANEXO",
                newName: "IX_ANEXO_ID_TIPO_DOCUMENTO");

            migrationBuilder.AddColumn<string>(
                name: "EXTENSAO",
                table: "ANEXO",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_CLIENTE",
                table: "ANEXO",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ANEXO_ID_CLIENTE",
                table: "ANEXO",
                column: "ID_CLIENTE");

            migrationBuilder.AddForeignKey(
                name: "FK_ANEXO_CLIENTE_ID_CLIENTE",
                table: "ANEXO",
                column: "ID_CLIENTE",
                principalTable: "CLIENTE",
                principalColumn: "ID_CLIENTE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO",
                table: "ANEXO",
                column: "ID_TIPO_DOCUMENTO",
                principalTable: "TIPO_DOCUMENTO",
                principalColumn: "ID_TIPO_DOCUMENTO",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANEXO_CLIENTE_ID_CLIENTE",
                table: "ANEXO");

            migrationBuilder.DropForeignKey(
                name: "FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO",
                table: "ANEXO");

            migrationBuilder.DropIndex(
                name: "IX_ANEXO_ID_CLIENTE",
                table: "ANEXO");

            migrationBuilder.DropColumn(
                name: "EXTENSAO",
                table: "ANEXO");

            migrationBuilder.DropColumn(
                name: "ID_CLIENTE",
                table: "ANEXO");

            migrationBuilder.RenameColumn(
                name: "ID_TIPO_DOCUMENTO",
                table: "ANEXO",
                newName: "ID_TIPO");

            migrationBuilder.RenameIndex(
                name: "IX_ANEXO_ID_TIPO_DOCUMENTO",
                table: "ANEXO",
                newName: "IX_ANEXO_ID_TIPO");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_NUMERO_ID_TIPO_DOCUMENTO_ID_UNIDADE_FEDERATIVA",
                table: "DOCUMENTO_IDENTIFICACAO_CLIENTE",
                columns: new[] { "NUMERO", "ID_TIPO_DOCUMENTO", "ID_UNIDADE_FEDERATIVA" },
                unique: true,
                filter: "[NUMERO] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO",
                table: "ANEXO",
                column: "ID_TIPO",
                principalTable: "TIPO_DOCUMENTO",
                principalColumn: "ID_TIPO_DOCUMENTO",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

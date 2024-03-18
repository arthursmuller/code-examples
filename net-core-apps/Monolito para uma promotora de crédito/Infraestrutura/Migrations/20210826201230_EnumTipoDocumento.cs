using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class EnumTipoDocumento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO",
                table: "ANEXO");

            migrationBuilder.DropForeignKey(
                name: "FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO",
                table: "DOCUMENTO_IDENTIFICACAO_CLIENTE");

            migrationBuilder.AddColumn<int>(name: "ID_TEMP_TIPO_DOCUMENTO", table: "TIPO_DOCUMENTO", defaultValue: 0);
            migrationBuilder.Sql(@"UPDATE dbo.TIPO_DOCUMENTO SET ID_TEMP_TIPO_DOCUMENTO = ID_TIPO_DOCUMENTO");
            migrationBuilder.DropPrimaryKey(name: "PK_TIPO_DOCUMENTO", table: "TIPO_DOCUMENTO");
            migrationBuilder.DropColumn(name: "ID_TIPO_DOCUMENTO", table: "TIPO_DOCUMENTO");
            migrationBuilder.RenameColumn(name: "ID_TEMP_TIPO_DOCUMENTO", table: "TIPO_DOCUMENTO", "ID_TIPO_DOCUMENTO");
            migrationBuilder.AddPrimaryKey(name: "PK_TIPO_DOCUMENTO", "TIPO_DOCUMENTO", column: "ID_TIPO_DOCUMENTO");

            migrationBuilder.AddForeignKey(
                name: "FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO",
                table: "ANEXO",
                column: "ID_TIPO_DOCUMENTO",
                principalTable: "TIPO_DOCUMENTO",
                principalColumn: "ID_TIPO_DOCUMENTO");

            migrationBuilder.AddForeignKey(
                name: "FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO",
                table: "DOCUMENTO_IDENTIFICACAO_CLIENTE",
                column: "ID_TIPO_DOCUMENTO",
                principalTable: "TIPO_DOCUMENTO",
                principalColumn: "ID_TIPO_DOCUMENTO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID_TIPO_DOCUMENTO",
                table: "TIPO_DOCUMENTO",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}

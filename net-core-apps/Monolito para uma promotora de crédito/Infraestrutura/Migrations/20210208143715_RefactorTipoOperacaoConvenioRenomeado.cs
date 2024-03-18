using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class RefactorTipoOperacaoConvenioRenomeado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CONVENIO_ORGAO_CONVENIO_ID_CONVENIO",
                table: "CONVENIO_ORGAO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_LEAD_CONVENIO_ID_CONVENIO",
                table: "LEAD");

            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETRO_OPERACAO_CONVENIO_ID_CONVENIO",
                table: "PARAMETRO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETRO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO",
                table: "PARAMETRO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_RENDIMENTO_CLIENTE_CONVENIO_ID_CONVENIO",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.AddColumn<int>(name: "ID_TEMP_TIPO_OPERACAO", table: "TIPO_OPERACAO", defaultValue: 0);
            migrationBuilder.Sql(@"UPDATE dbo.TIPO_OPERACAO SET ID_TEMP_TIPO_OPERACAO = ID_TIPO_OPERACAO");
            migrationBuilder.DropPrimaryKey(name: "PK_TIPO_OPERACAO", table: "TIPO_OPERACAO");
            migrationBuilder.DropColumn(name: "ID_TIPO_OPERACAO", table: "TIPO_OPERACAO");
            migrationBuilder.RenameColumn(name: "ID_TEMP_TIPO_OPERACAO", table: "TIPO_OPERACAO", "ID_TIPO_OPERACAO");
            migrationBuilder.AddPrimaryKey(name: "PK_TIPO_OPERACAO", "TIPO_OPERACAO", column: "ID_TIPO_OPERACAO");

            migrationBuilder.AddColumn<int>(name: "ID_TEMP_CONVENIO", table: "CONVENIO", defaultValue: 0);
            migrationBuilder.Sql(@"UPDATE dbo.CONVENIO SET ID_TEMP_CONVENIO = ID_CONVENIO");
            migrationBuilder.DropPrimaryKey(name: "PK_CONVENIO", table: "CONVENIO");
            migrationBuilder.DropColumn(name: "ID_CONVENIO", table: "CONVENIO");
            migrationBuilder.RenameColumn(name: "ID_TEMP_CONVENIO", table: "CONVENIO", "ID_CONVENIO");
            migrationBuilder.AddPrimaryKey(name: "PK_CONVENIO", "CONVENIO", column: "ID_CONVENIO");            

            migrationBuilder.AddForeignKey(
                name: "FK_CONVENIO_ORGAO_CONVENIO_ID_CONVENIO",
                table: "CONVENIO_ORGAO",
                column: "ID_CONVENIO",
                principalTable: "CONVENIO",
                principalColumn: "ID_CONVENIO");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO",
                table: "INTENCAO_OPERACAO",
                column: "ID_TIPO_OPERACAO",
                principalTable: "TIPO_OPERACAO",
                principalColumn: "ID_TIPO_OPERACAO");

            migrationBuilder.AddForeignKey(
                name: "FK_LEAD_CONVENIO_ID_CONVENIO",
                table: "LEAD",
                column: "ID_CONVENIO",
                principalTable: "CONVENIO",
                principalColumn: "ID_CONVENIO");

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETRO_OPERACAO_CONVENIO_ID_CONVENIO",
                table: "PARAMETRO_OPERACAO",
                column: "ID_CONVENIO",
                principalTable: "CONVENIO",
                principalColumn: "ID_CONVENIO");

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETRO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO",
                table: "PARAMETRO_OPERACAO",
                column: "ID_TIPO_OPERACAO",
                principalTable: "TIPO_OPERACAO",
                principalColumn: "ID_TIPO_OPERACAO");

            migrationBuilder.AddForeignKey(
                name: "FK_RENDIMENTO_CLIENTE_CONVENIO_ID_CONVENIO",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_CONVENIO",
                principalTable: "CONVENIO",
                principalColumn: "ID_CONVENIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID_TIPO_OPERACAO",
                table: "TIPO_OPERACAO",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ID_CONVENIO",
                table: "CONVENIO",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}

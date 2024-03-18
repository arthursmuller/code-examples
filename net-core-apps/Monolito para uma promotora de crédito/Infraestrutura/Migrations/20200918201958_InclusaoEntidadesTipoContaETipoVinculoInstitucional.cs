using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class InclusaoEntidadesTipoContaETipoVinculoInstitucional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposVinculoInsticional",
                table: "TiposVinculoInsticional");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposConta",
                table: "TiposConta");

            migrationBuilder.RenameTable(
                name: "TiposVinculoInsticional",
                newName: "TIPOS_VINCULO_INSTITUCIONAL");

            migrationBuilder.RenameTable(
                name: "TiposConta",
                newName: "TIPOS_CONTA");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TIPOS_VINCULO_INSTITUCIONAL",
                newName: "NOME");

            migrationBuilder.RenameColumn(
                name: "UsuarioAtualizacao",
                table: "TIPOS_VINCULO_INSTITUCIONAL",
                newName: "USUARIO_ATUALIZACAO");

            migrationBuilder.RenameColumn(
                name: "DataAtualizacao",
                table: "TIPOS_VINCULO_INSTITUCIONAL",
                newName: "DATA_ATUALIZACAO");

            migrationBuilder.RenameColumn(
                name: "Sigla",
                table: "TIPOS_CONTA",
                newName: "SIGLA");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TIPOS_CONTA",
                newName: "NOME");

            migrationBuilder.RenameColumn(
                name: "UsuarioAtualizacao",
                table: "TIPOS_CONTA",
                newName: "USUARIO_ATUALIZACAO");

            migrationBuilder.RenameColumn(
                name: "DataAtualizacao",
                table: "TIPOS_CONTA",
                newName: "DATA_ATUALIZACAO");

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                table: "TIPOS_VINCULO_INSTITUCIONAL",
                maxLength: 8000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "TIPOS_VINCULO_INSTITUCIONAL",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SIGLA",
                table: "TIPOS_CONTA",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                table: "TIPOS_CONTA",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "TIPOS_CONTA",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIPOS_VINCULO_INSTITUCIONAL",
                table: "TIPOS_VINCULO_INSTITUCIONAL",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIPOS_CONTA",
                table: "TIPOS_CONTA",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TIPOS_VINCULO_INSTITUCIONAL",
                table: "TIPOS_VINCULO_INSTITUCIONAL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIPOS_CONTA",
                table: "TIPOS_CONTA");

            migrationBuilder.RenameTable(
                name: "TIPOS_VINCULO_INSTITUCIONAL",
                newName: "TiposVinculoInsticional");

            migrationBuilder.RenameTable(
                name: "TIPOS_CONTA",
                newName: "TiposConta");

            migrationBuilder.RenameColumn(
                name: "NOME",
                table: "TiposVinculoInsticional",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "TiposVinculoInsticional",
                newName: "UsuarioAtualizacao");

            migrationBuilder.RenameColumn(
                name: "DATA_ATUALIZACAO",
                table: "TiposVinculoInsticional",
                newName: "DataAtualizacao");

            migrationBuilder.RenameColumn(
                name: "SIGLA",
                table: "TiposConta",
                newName: "Sigla");

            migrationBuilder.RenameColumn(
                name: "NOME",
                table: "TiposConta",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "TiposConta",
                newName: "UsuarioAtualizacao");

            migrationBuilder.RenameColumn(
                name: "DATA_ATUALIZACAO",
                table: "TiposConta",
                newName: "DataAtualizacao");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "TiposVinculoInsticional",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8000);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioAtualizacao",
                table: "TiposVinculoInsticional",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sigla",
                table: "TiposConta",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "TiposConta",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioAtualizacao",
                table: "TiposConta",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposVinculoInsticional",
                table: "TiposVinculoInsticional",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposConta",
                table: "TiposConta",
                column: "ID");
        }
    }
}

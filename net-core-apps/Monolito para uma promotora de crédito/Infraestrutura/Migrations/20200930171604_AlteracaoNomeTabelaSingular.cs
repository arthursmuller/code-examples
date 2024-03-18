using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AlteracaoNomeTabelaSingular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANEXOS_USUARIOS_IdUsuario",
                table: "ANEXOS");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCOES_OPERACAO_LEADS_ID_LEAD",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCOES_OPERACAO_LOJAS_ID_LOJA",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCOES_OPERACAO_SITUACOES_INTENCAO_OPERACAO_ID_SITUACAO",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCOES_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCOES_OPERACAO_USUARIOS_ID_USUARIO",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_LEADS_CONVENIOS_ID_CONVENIO",
                table: "LEADS");

            migrationBuilder.DropForeignKey(
                name: "FK_LEADS_LOJAS_ID_LOJA",
                table: "LEADS");

            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_ID_CONVENIO",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_TELEFONES_LOJAS_LOJAS_ID_LOJA",
                table: "TELEFONES_LOJAS");

            migrationBuilder.DropForeignKey(
                name: "FK_USUARIOS_LOJAS_ID_LOJA",
                table: "USUARIOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USUARIOS",
                table: "USUARIOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIPOS_VINCULO_INSTITUCIONAL",
                table: "TIPOS_VINCULO_INSTITUCIONAL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIPOS_OPERACAO",
                table: "TIPOS_OPERACAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIPOS_CONTA",
                table: "TIPOS_CONTA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TELEFONES_LOJAS",
                table: "TELEFONES_LOJAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SITUACOES_INTENCAO_OPERACAO",
                table: "SITUACOES_INTENCAO_OPERACAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUTOS",
                table: "PRODUTOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PARAMETROS_OPERACAO",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LOJAS",
                table: "LOJAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LEADS",
                table: "LEADS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_INTENCOES_OPERACAO",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CONVENIOS",
                table: "CONVENIOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ANEXOS",
                table: "ANEXOS");

            migrationBuilder.RenameTable(
                name: "USUARIOS",
                newName: "USUARIO");

            migrationBuilder.RenameTable(
                name: "TIPOS_VINCULO_INSTITUCIONAL",
                newName: "TIPO_VINCULO_INSTITUCIONAL");

            migrationBuilder.RenameTable(
                name: "TIPOS_OPERACAO",
                newName: "TIPO_OPERACAO");

            migrationBuilder.RenameTable(
                name: "TIPOS_CONTA",
                newName: "TIPO_CONTA");

            migrationBuilder.RenameTable(
                name: "TELEFONES_LOJAS",
                newName: "TELEFONE_LOJA");

            migrationBuilder.RenameTable(
                name: "SITUACOES_INTENCAO_OPERACAO",
                newName: "SITUACAO_INTENCAO_OPERACAO");

            migrationBuilder.RenameTable(
                name: "PRODUTOS",
                newName: "PRODUTO");

            migrationBuilder.RenameTable(
                name: "PARAMETROS_OPERACAO",
                newName: "PARAMETRO_OPERACAO");

            migrationBuilder.RenameTable(
                name: "LOJAS",
                newName: "LOJA");

            migrationBuilder.RenameTable(
                name: "LEADS",
                newName: "LEAD");

            migrationBuilder.RenameTable(
                name: "INTENCOES_OPERACAO",
                newName: "INTENCAO_OPERACAO");

            migrationBuilder.RenameTable(
                name: "CONVENIOS",
                newName: "CONVENIO");

            migrationBuilder.RenameTable(
                name: "ANEXOS",
                newName: "ANEXO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "USUARIO",
                newName: "ID_USUARIO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TIPO_VINCULO_INSTITUCIONAL",
                newName: "ID_TIPO_VINCULO_INSTITUCIONAL");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TIPO_OPERACAO",
                newName: "ID_TIPO_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TIPO_CONTA",
                newName: "ID_TIPO_CONTA");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TELEFONE_LOJA",
                newName: "ID_TELEFONE_LOJA");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "SITUACAO_INTENCAO_OPERACAO",
                newName: "ID_SITUACAO_INTENCAO_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PRODUTO",
                newName: "ID_PRODUTO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PARAMETRO_OPERACAO",
                newName: "ID_PARAMETRO_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "LOJA",
                newName: "ID_LOJA");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "LEAD",
                newName: "ID_LEAD");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "INTENCAO_OPERACAO",
                newName: "ID_INTENCAO_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CONVENIO",
                newName: "ID_CONVENIO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ANEXO",
                newName: "ID_ANEXO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USUARIO",
                table: "USUARIO",
                column: "ID_USUARIO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIPO_VINCULO_INSTITUCIONAL",
                table: "TIPO_VINCULO_INSTITUCIONAL",
                column: "ID_TIPO_VINCULO_INSTITUCIONAL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIPO_OPERACAO",
                table: "TIPO_OPERACAO",
                column: "ID_TIPO_OPERACAO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIPO_CONTA",
                table: "TIPO_CONTA",
                column: "ID_TIPO_CONTA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TELEFONE_LOJA",
                table: "TELEFONE_LOJA",
                column: "ID_TELEFONE_LOJA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SITUACAO_INTENCAO_OPERACAO",
                table: "SITUACAO_INTENCAO_OPERACAO",
                column: "ID_SITUACAO_INTENCAO_OPERACAO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUTO",
                table: "PRODUTO",
                column: "ID_PRODUTO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PARAMETRO_OPERACAO",
                table: "PARAMETRO_OPERACAO",
                column: "ID_PARAMETRO_OPERACAO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LOJA",
                table: "LOJA",
                column: "ID_LOJA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LEAD",
                table: "LEAD",
                column: "ID_LEAD");

            migrationBuilder.AddPrimaryKey(
                name: "PK_INTENCAO_OPERACAO",
                table: "INTENCAO_OPERACAO",
                column: "ID_INTENCAO_OPERACAO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CONVENIO",
                table: "CONVENIO",
                column: "ID_CONVENIO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ANEXO",
                table: "ANEXO",
                column: "ID_ANEXO");

            migrationBuilder.AddForeignKey(
                name: "FK_ANEXO_USUARIO_IdUsuario",
                table: "ANEXO",
                column: "IdUsuario",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_LEAD_ID_LEAD",
                table: "INTENCAO_OPERACAO",
                column: "ID_LEAD",
                principalTable: "LEAD",
                principalColumn: "ID_LEAD");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_LOJA_ID_LOJA",
                table: "INTENCAO_OPERACAO",
                column: "ID_LOJA",
                principalTable: "LOJA",
                principalColumn: "ID_LOJA");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_SITUACAO_INTENCAO_OPERACAO_ID_SITUACAO",
                table: "INTENCAO_OPERACAO",
                column: "ID_SITUACAO",
                principalTable: "SITUACAO_INTENCAO_OPERACAO",
                principalColumn: "ID_SITUACAO_INTENCAO_OPERACAO");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO",
                table: "INTENCAO_OPERACAO",
                column: "ID_TIPO_OPERACAO",
                principalTable: "TIPO_OPERACAO",
                principalColumn: "ID_TIPO_OPERACAO");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_USUARIO_ID_USUARIO",
                table: "INTENCAO_OPERACAO",
                column: "ID_USUARIO",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO");

            migrationBuilder.AddForeignKey(
                name: "FK_LEAD_CONVENIO_ID_CONVENIO",
                table: "LEAD",
                column: "ID_CONVENIO",
                principalTable: "CONVENIO",
                principalColumn: "ID_CONVENIO");

            migrationBuilder.AddForeignKey(
                name: "FK_LEAD_LOJA_ID_LOJA",
                table: "LEAD",
                column: "ID_LOJA",
                principalTable: "LOJA",
                principalColumn: "ID_LOJA");

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
                name: "FK_TELEFONE_LOJA_LOJA_ID_LOJA",
                table: "TELEFONE_LOJA",
                column: "ID_LOJA",
                principalTable: "LOJA",
                principalColumn: "ID_LOJA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_LOJA_ID_LOJA",
                table: "USUARIO",
                column: "ID_LOJA",
                principalTable: "LOJA",
                principalColumn: "ID_LOJA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANEXO_USUARIO_IdUsuario",
                table: "ANEXO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_LEAD_ID_LEAD",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_LOJA_ID_LOJA",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_SITUACAO_INTENCAO_OPERACAO_ID_SITUACAO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_USUARIO_ID_USUARIO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_LEAD_CONVENIO_ID_CONVENIO",
                table: "LEAD");

            migrationBuilder.DropForeignKey(
                name: "FK_LEAD_LOJA_ID_LOJA",
                table: "LEAD");

            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETRO_OPERACAO_CONVENIO_ID_CONVENIO",
                table: "PARAMETRO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETRO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO",
                table: "PARAMETRO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_TELEFONE_LOJA_LOJA_ID_LOJA",
                table: "TELEFONE_LOJA");

            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_LOJA_ID_LOJA",
                table: "USUARIO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USUARIO",
                table: "USUARIO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIPO_VINCULO_INSTITUCIONAL",
                table: "TIPO_VINCULO_INSTITUCIONAL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIPO_OPERACAO",
                table: "TIPO_OPERACAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIPO_CONTA",
                table: "TIPO_CONTA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TELEFONE_LOJA",
                table: "TELEFONE_LOJA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SITUACAO_INTENCAO_OPERACAO",
                table: "SITUACAO_INTENCAO_OPERACAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUTO",
                table: "PRODUTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PARAMETRO_OPERACAO",
                table: "PARAMETRO_OPERACAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LOJA",
                table: "LOJA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LEAD",
                table: "LEAD");

            migrationBuilder.DropPrimaryKey(
                name: "PK_INTENCAO_OPERACAO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CONVENIO",
                table: "CONVENIO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ANEXO",
                table: "ANEXO");

            migrationBuilder.RenameTable(
                name: "USUARIO",
                newName: "USUARIOS");

            migrationBuilder.RenameTable(
                name: "TIPO_VINCULO_INSTITUCIONAL",
                newName: "TIPOS_VINCULO_INSTITUCIONAL");

            migrationBuilder.RenameTable(
                name: "TIPO_OPERACAO",
                newName: "TIPOS_OPERACAO");

            migrationBuilder.RenameTable(
                name: "TIPO_CONTA",
                newName: "TIPOS_CONTA");

            migrationBuilder.RenameTable(
                name: "TELEFONE_LOJA",
                newName: "TELEFONES_LOJAS");

            migrationBuilder.RenameTable(
                name: "SITUACAO_INTENCAO_OPERACAO",
                newName: "SITUACOES_INTENCAO_OPERACAO");

            migrationBuilder.RenameTable(
                name: "PRODUTO",
                newName: "PRODUTOS");

            migrationBuilder.RenameTable(
                name: "PARAMETRO_OPERACAO",
                newName: "PARAMETROS_OPERACAO");

            migrationBuilder.RenameTable(
                name: "LOJA",
                newName: "LOJAS");

            migrationBuilder.RenameTable(
                name: "LEAD",
                newName: "LEADS");

            migrationBuilder.RenameTable(
                name: "INTENCAO_OPERACAO",
                newName: "INTENCOES_OPERACAO");

            migrationBuilder.RenameTable(
                name: "CONVENIO",
                newName: "CONVENIOS");

            migrationBuilder.RenameTable(
                name: "ANEXO",
                newName: "ANEXOS");

            migrationBuilder.RenameColumn(
                name: "ID_USUARIO",
                table: "USUARIOS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_TIPO_VINCULO_INSTITUCIONAL",
                table: "TIPOS_VINCULO_INSTITUCIONAL",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_TIPO_OPERACAO",
                table: "TIPOS_OPERACAO",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_TIPO_CONTA",
                table: "TIPOS_CONTA",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_TELEFONE_LOJA",
                table: "TELEFONES_LOJAS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_SITUACAO_INTENCAO_OPERACAO",
                table: "SITUACOES_INTENCAO_OPERACAO",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_PRODUTO",
                table: "PRODUTOS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_PARAMETRO_OPERACAO",
                table: "PARAMETROS_OPERACAO",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_LOJA",
                table: "LOJAS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_LEAD",
                table: "LEADS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_INTENCAO_OPERACAO",
                table: "INTENCOES_OPERACAO",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_CONVENIO",
                table: "CONVENIOS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_ANEXO",
                table: "ANEXOS",
                newName: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USUARIOS",
                table: "USUARIOS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIPOS_VINCULO_INSTITUCIONAL",
                table: "TIPOS_VINCULO_INSTITUCIONAL",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIPOS_OPERACAO",
                table: "TIPOS_OPERACAO",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIPOS_CONTA",
                table: "TIPOS_CONTA",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TELEFONES_LOJAS",
                table: "TELEFONES_LOJAS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SITUACOES_INTENCAO_OPERACAO",
                table: "SITUACOES_INTENCAO_OPERACAO",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUTOS",
                table: "PRODUTOS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PARAMETROS_OPERACAO",
                table: "PARAMETROS_OPERACAO",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LOJAS",
                table: "LOJAS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LEADS",
                table: "LEADS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_INTENCOES_OPERACAO",
                table: "INTENCOES_OPERACAO",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CONVENIOS",
                table: "CONVENIOS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ANEXOS",
                table: "ANEXOS",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ANEXOS_USUARIOS_IdUsuario",
                table: "ANEXOS",
                column: "IdUsuario",
                principalTable: "USUARIOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCOES_OPERACAO_LEADS_ID_LEAD",
                table: "INTENCOES_OPERACAO",
                column: "ID_LEAD",
                principalTable: "LEADS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCOES_OPERACAO_LOJAS_ID_LOJA",
                table: "INTENCOES_OPERACAO",
                column: "ID_LOJA",
                principalTable: "LOJAS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCOES_OPERACAO_SITUACOES_INTENCAO_OPERACAO_ID_SITUACAO",
                table: "INTENCOES_OPERACAO",
                column: "ID_SITUACAO",
                principalTable: "SITUACOES_INTENCAO_OPERACAO",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCOES_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO",
                table: "INTENCOES_OPERACAO",
                column: "ID_TIPO_OPERACAO",
                principalTable: "TIPOS_OPERACAO",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCOES_OPERACAO_USUARIOS_ID_USUARIO",
                table: "INTENCOES_OPERACAO",
                column: "ID_USUARIO",
                principalTable: "USUARIOS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LEADS_CONVENIOS_ID_CONVENIO",
                table: "LEADS",
                column: "ID_CONVENIO",
                principalTable: "CONVENIOS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LEADS_LOJAS_ID_LOJA",
                table: "LEADS",
                column: "ID_LOJA",
                principalTable: "LOJAS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_ID_CONVENIO",
                table: "PARAMETROS_OPERACAO",
                column: "ID_CONVENIO",
                principalTable: "CONVENIOS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO",
                table: "PARAMETROS_OPERACAO",
                column: "ID_TIPO_OPERACAO",
                principalTable: "TIPOS_OPERACAO",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TELEFONES_LOJAS_LOJAS_ID_LOJA",
                table: "TELEFONES_LOJAS",
                column: "ID_LOJA",
                principalTable: "LOJAS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIOS_LOJAS_ID_LOJA",
                table: "USUARIOS",
                column: "ID_LOJA",
                principalTable: "LOJAS",
                principalColumn: "ID");
        }
    }
}

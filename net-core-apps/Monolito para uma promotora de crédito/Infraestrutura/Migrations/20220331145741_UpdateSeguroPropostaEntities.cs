using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class UpdateSeguroPropostaEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SEGURO_COBRANCA_PROPOSTA_ICATU_SEGURO_PROPOSTA_ICATU_IdSeguroPropostaIcatu",
                table: "SEGURO_COBRANCA_PROPOSTA_ICATU");

            migrationBuilder.DropForeignKey(
                name: "FK_SEGURO_ENDERECO_CLIENTE_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_ENDERECO_CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_SEGURO_ENDERECO_CLIENTE_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_ENDERECO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_ENDERECO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "Principal",
                table: "SEGURO_ENDERECO_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "IdSeguroPropostaIcatu",
                table: "SEGURO_COBRANCA_PROPOSTA_ICATU",
                newName: "ID_SEGURO_PROPOSTA_ICATU");

            migrationBuilder.RenameIndex(
                name: "IX_SEGURO_COBRANCA_PROPOSTA_ICATU_IdSeguroPropostaIcatu",
                table: "SEGURO_COBRANCA_PROPOSTA_ICATU",
                newName: "IX_SEGURO_COBRANCA_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA_ICATU");

            migrationBuilder.AlterColumn<int>(
                name: "NUMERO",
                table: "SEGURO_ENDERECO_CLIENTE",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ID_COBRANCA",
                table: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU",
                type: "varchar(4000)",
                unicode: false,
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ID_CARTAO",
                table: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU",
                type: "varchar(4000)",
                unicode: false,
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF_TITULAR",
                table: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU",
                type: "varchar(14)",
                unicode: false,
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TITULAR",
                table: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_NASCIMENTO",
                table: "SEGURO_CLIENTE_ICATU",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "ID_ENDERECO_COBRANCA",
                table: "SEGURO_CLIENTE_ICATU",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_ENDERECO_PRINCIPAL",
                table: "SEGURO_CLIENTE_ICATU",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_PROFISSAO",
                table: "CLIENTE",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_CLIENTE_ICATU_ID_ENDERECO_COBRANCA",
                table: "SEGURO_CLIENTE_ICATU",
                column: "ID_ENDERECO_COBRANCA");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_CLIENTE_ICATU_ID_ENDERECO_PRINCIPAL",
                table: "SEGURO_CLIENTE_ICATU",
                column: "ID_ENDERECO_PRINCIPAL");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_PROFISSAO",
                table: "CLIENTE",
                column: "ID_PROFISSAO");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_SEGURO_PROFISSAO_ID_PROFISSAO",
                table: "CLIENTE",
                column: "ID_PROFISSAO",
                principalTable: "SEGURO_PROFISSAO",
                principalColumn: "ID_SEGURO_PROFISSAO");

            migrationBuilder.AddForeignKey(
                name: "FK_SEGURO_CLIENTE_ICATU_SEGURO_ENDERECO_CLIENTE_ID_ENDERECO_COBRANCA",
                table: "SEGURO_CLIENTE_ICATU",
                column: "ID_ENDERECO_COBRANCA",
                principalTable: "SEGURO_ENDERECO_CLIENTE",
                principalColumn: "ID_SEGURO_ENDERECO_CLIENTE",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SEGURO_CLIENTE_ICATU_SEGURO_ENDERECO_CLIENTE_ID_ENDERECO_PRINCIPAL",
                table: "SEGURO_CLIENTE_ICATU",
                column: "ID_ENDERECO_PRINCIPAL",
                principalTable: "SEGURO_ENDERECO_CLIENTE",
                principalColumn: "ID_SEGURO_ENDERECO_CLIENTE",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SEGURO_COBRANCA_PROPOSTA_ICATU_SEGURO_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA_ICATU",
                table: "SEGURO_COBRANCA_PROPOSTA_ICATU",
                column: "ID_SEGURO_PROPOSTA_ICATU",
                principalTable: "SEGURO_PROPOSTA_ICATU",
                principalColumn: "ID_SEGURO_PROPOSTA_ICATU",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_SEGURO_PROFISSAO_ID_PROFISSAO",
                table: "CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_SEGURO_CLIENTE_ICATU_SEGURO_ENDERECO_CLIENTE_ID_ENDERECO_COBRANCA",
                table: "SEGURO_CLIENTE_ICATU");

            migrationBuilder.DropForeignKey(
                name: "FK_SEGURO_CLIENTE_ICATU_SEGURO_ENDERECO_CLIENTE_ID_ENDERECO_PRINCIPAL",
                table: "SEGURO_CLIENTE_ICATU");

            migrationBuilder.DropForeignKey(
                name: "FK_SEGURO_COBRANCA_PROPOSTA_ICATU_SEGURO_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA_ICATU",
                table: "SEGURO_COBRANCA_PROPOSTA_ICATU");

            migrationBuilder.DropIndex(
                name: "IX_SEGURO_CLIENTE_ICATU_ID_ENDERECO_COBRANCA",
                table: "SEGURO_CLIENTE_ICATU");

            migrationBuilder.DropIndex(
                name: "IX_SEGURO_CLIENTE_ICATU_ID_ENDERECO_PRINCIPAL",
                table: "SEGURO_CLIENTE_ICATU");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_PROFISSAO",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "CPF_TITULAR",
                table: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU");

            migrationBuilder.DropColumn(
                name: "TITULAR",
                table: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU");

            migrationBuilder.DropColumn(
                name: "ID_ENDERECO_COBRANCA",
                table: "SEGURO_CLIENTE_ICATU");

            migrationBuilder.DropColumn(
                name: "ID_ENDERECO_PRINCIPAL",
                table: "SEGURO_CLIENTE_ICATU");

            migrationBuilder.DropColumn(
                name: "ID_PROFISSAO",
                table: "CLIENTE");

            migrationBuilder.RenameColumn(
                name: "ID_SEGURO_PROPOSTA_ICATU",
                table: "SEGURO_COBRANCA_PROPOSTA_ICATU",
                newName: "IdSeguroPropostaIcatu");

            migrationBuilder.RenameIndex(
                name: "IX_SEGURO_COBRANCA_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA_ICATU",
                table: "SEGURO_COBRANCA_PROPOSTA_ICATU",
                newName: "IX_SEGURO_COBRANCA_PROPOSTA_ICATU_IdSeguroPropostaIcatu");

            migrationBuilder.AlterColumn<int>(
                name: "NUMERO",
                table: "SEGURO_ENDERECO_CLIENTE",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_ENDERECO_CLIENTE",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Principal",
                table: "SEGURO_ENDERECO_CLIENTE",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ID_COBRANCA",
                table: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4000)",
                oldUnicode: false,
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ID_CARTAO",
                table: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4000)",
                oldUnicode: false,
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_NASCIMENTO",
                table: "SEGURO_CLIENTE_ICATU",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_ENDERECO_CLIENTE_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_ENDERECO_CLIENTE",
                column: "ID_SEGURO_CLIENTE_ICATU");

            migrationBuilder.AddForeignKey(
                name: "FK_SEGURO_COBRANCA_PROPOSTA_ICATU_SEGURO_PROPOSTA_ICATU_IdSeguroPropostaIcatu",
                table: "SEGURO_COBRANCA_PROPOSTA_ICATU",
                column: "IdSeguroPropostaIcatu",
                principalTable: "SEGURO_PROPOSTA_ICATU",
                principalColumn: "ID_SEGURO_PROPOSTA_ICATU",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SEGURO_ENDERECO_CLIENTE_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_ENDERECO_CLIENTE",
                column: "ID_SEGURO_CLIENTE_ICATU",
                principalTable: "SEGURO_CLIENTE_ICATU",
                principalColumn: "ID_SEGURO_CLIENTE_ICATU",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

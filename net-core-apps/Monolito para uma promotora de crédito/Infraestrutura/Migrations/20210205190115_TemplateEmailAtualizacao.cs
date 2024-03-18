using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class TemplateEmailAtualizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                keyColumnType: "int",
                table: "TEMPLATE_EMAIL_FINALIDADE",
                keyColumn: "ID",
                keyValue: 0);

            migrationBuilder.DeleteData(
                keyColumnType: "int",
                table: "TEMPLATE_EMAIL_FINALIDADE",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                keyColumnType: "int",
                table: "TEMPLATE_EMAIL_TIPO",
                keyColumn: "ID",
                keyValue: 0);

            migrationBuilder.DeleteData(
                keyColumnType: "int",
                table: "TEMPLATE_EMAIL_TIPO",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                keyColumnType: "int",
                table: "TEMPLATE_EMAIL_TIPO",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TEMPLATE_EMAIL_TIPO",
                newName: "ID_TEMPLATE_EMAIL_TIPO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TEMPLATE_EMAIL_FINALIDADE",
                newName: "ID_TEMPLATE_EMAIL_FINALIDADE");

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "TEMPLATE_EMAIL_TIPO",
                unicode: false,
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "TEMPLATE_EMAIL_FINALIDADE",
                unicode: false,
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "TEMPLATE_EMAIL_TIPO");

            migrationBuilder.DropColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "TEMPLATE_EMAIL_FINALIDADE");

            migrationBuilder.RenameColumn(
                name: "ID_TEMPLATE_EMAIL_TIPO",
                table: "TEMPLATE_EMAIL_TIPO",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ID_TEMPLATE_EMAIL_FINALIDADE",
                table: "TEMPLATE_EMAIL_FINALIDADE",
                newName: "ID");

            migrationBuilder.InsertData(
                table: "TEMPLATE_EMAIL_FINALIDADE",
                columns: new[] { "ID", "DATA_ATUALIZACAO", "DESCRICAO" },
                values: new object[,]
                {
                    { 0, new DateTime(2021, 2, 1, 9, 18, 11, 146, DateTimeKind.Local).AddTicks(5820), "Default" },
                    { 1, new DateTime(2021, 2, 1, 9, 18, 11, 146, DateTimeKind.Local).AddTicks(6380), "RecuperacaoSenha" }
                });

            migrationBuilder.InsertData(
                table: "TEMPLATE_EMAIL_TIPO",
                columns: new[] { "ID", "DATA_ATUALIZACAO", "DESCRICAO" },
                values: new object[,]
                {
                    { 0, new DateTime(2021, 2, 1, 9, 18, 11, 128, DateTimeKind.Local).AddTicks(210), "Content" },
                    { 1, new DateTime(2021, 2, 1, 9, 18, 11, 143, DateTimeKind.Local).AddTicks(8020), "Header" },
                    { 2, new DateTime(2021, 2, 1, 9, 18, 11, 143, DateTimeKind.Local).AddTicks(8300), "Footer" }
                });
        }
    }
}

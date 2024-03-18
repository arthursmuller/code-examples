using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class TemplateEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TEMPLATE_EMAIL_FINALIDADE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    DESCRICAO = table.Column<string>(nullable: false),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATE_EMAIL_FINALIDADE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TEMPLATE_EMAIL_TIPO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    DESCRICAO = table.Column<string>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATE_EMAIL_TIPO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TEMPLATE_EMAIL",
                columns: table => new
                {
                    ID_TEMPLATE_EMAIL = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    CONTEUDO = table.Column<string>(nullable: false),
                    TIPO = table.Column<int>(nullable: false),
                    FINALIDADE = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATE_EMAIL", x => x.ID_TEMPLATE_EMAIL);
                    table.ForeignKey(
                        name: "FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_FINALIDADE_FINALIDADE",
                        column: x => x.FINALIDADE,
                        principalTable: "TEMPLATE_EMAIL_FINALIDADE",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_TIPO_TIPO",
                        column: x => x.TIPO,
                        principalTable: "TEMPLATE_EMAIL_TIPO",
                        principalColumn: "ID");
                });

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
                columns: new[] { "ID", "DataAtualizacao", "DESCRICAO" },
                values: new object[,]
                {
                    { 0, new DateTime(2021, 2, 1, 9, 18, 11, 128, DateTimeKind.Local).AddTicks(210), "Content" },
                    { 1, new DateTime(2021, 2, 1, 9, 18, 11, 143, DateTimeKind.Local).AddTicks(8020), "Header" },
                    { 2, new DateTime(2021, 2, 1, 9, 18, 11, 143, DateTimeKind.Local).AddTicks(8300), "Footer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TEMPLATE_EMAIL_FINALIDADE",
                table: "TEMPLATE_EMAIL",
                column: "FINALIDADE");

            migrationBuilder.CreateIndex(
                name: "IX_TEMPLATE_EMAIL_TIPO_FINALIDADE",
                table: "TEMPLATE_EMAIL",
                columns: new[] { "TIPO", "FINALIDADE" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TEMPLATE_EMAIL");

            migrationBuilder.DropTable(
                name: "TEMPLATE_EMAIL_FINALIDADE");

            migrationBuilder.DropTable(
                name: "TEMPLATE_EMAIL_TIPO");
        }
    }
}

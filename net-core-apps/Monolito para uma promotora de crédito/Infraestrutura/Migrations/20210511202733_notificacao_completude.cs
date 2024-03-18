using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class notificacao_completude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "COMPLETO",
                table: "NOTIFICACAO",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_CRIACAO",
                table: "NOTIFICACAO",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "COMPLETO",
                table: "NOTIFICACAO");

            migrationBuilder.DropColumn(
                name: "DATA_CRIACAO",
                table: "NOTIFICACAO");
        }
    }
}

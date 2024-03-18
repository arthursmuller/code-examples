using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class ContasClienteRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RENDIMENTO_CLIENTE_BANCO_ID_BANCO",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_RENDIMENTO_CLIENTE_TIPO_CONTA_ID_TIPO_CONTA",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "AGENCIA",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "CONTA",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_TIPO_CONTA",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_BANCO",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_TIPO_CONTA",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_BANCO",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.AddColumn<int>(
                name: "ID_CONTA_CLIENTE",
                table: "RENDIMENTO_CLIENTE",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "ID_CONTA_CLIENTE_RECEBIMENTO",
                table: "RENDIMENTO_CLIENTE",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_CONTA_CLIENTE",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_CONTA_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_CONTA_CLIENTE_RECEBIMENTO",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_CONTA_CLIENTE_RECEBIMENTO");

            migrationBuilder.AddColumn<int>(name: "ID_TEMP_TIPO_CONTA", table: "TIPO_CONTA", defaultValue: 0);
            migrationBuilder.Sql(@"UPDATE dbo.TIPO_CONTA SET ID_TEMP_TIPO_CONTA = ID_TIPO_CONTA");
            migrationBuilder.DropPrimaryKey(name: "PK_TIPO_CONTA", table: "TIPO_CONTA");
            migrationBuilder.DropColumn(name: "ID_TIPO_CONTA", table: "TIPO_CONTA");
            migrationBuilder.RenameColumn(name: "ID_TEMP_TIPO_CONTA", table: "TIPO_CONTA", "ID_TIPO_CONTA");
            migrationBuilder.AddPrimaryKey(name: "PK_TIPO_CONTA", table: "TIPO_CONTA", column: "ID_TIPO_CONTA");
            
            migrationBuilder.CreateTable(
                name: "FORMA_RECEBIMENTO",
                columns: table => new
                {
                    ID_FORMA_RECEBIMENTO = table.Column<int>(type: "int", nullable: false),
                    NOME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMA_RECEBIMENTO", x => x.ID_FORMA_RECEBIMENTO);
                });

            migrationBuilder.CreateTable(
                name: "CONTA_CLIENTE",
                columns: table => new
                {
                    ID_CONTA_CLIENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    ID_BANCO = table.Column<int>(type: "int", nullable: false),
                    ID_TIPO_CONTA = table.Column<int>(type: "int", nullable: false),
                    AGENCIA = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    CONTA = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    ID_FORMA_RECEBIMENTO = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTA_CLIENTE", x => x.ID_CONTA_CLIENTE);
                    table.ForeignKey(
                        name: "FK_CONTA_CLIENTE_BANCO_ID_BANCO",
                        column: x => x.ID_BANCO,
                        principalTable: "BANCO",
                        principalColumn: "ID_BANCO");
                    table.ForeignKey(
                        name: "FK_CONTA_CLIENTE_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE");
                    table.ForeignKey(
                        name: "FK_CONTA_CLIENTE_FORMA_RECEBIMENTO_ID_FORMA_RECEBIMENTO",
                        column: x => x.ID_FORMA_RECEBIMENTO,
                        principalTable: "FORMA_RECEBIMENTO",
                        principalColumn: "ID_FORMA_RECEBIMENTO");
                    table.ForeignKey(
                        name: "FK_CONTA_CLIENTE_TIPO_CONTA_ID_TIPO_CONTA",
                        column: x => x.ID_TIPO_CONTA,
                        principalTable: "TIPO_CONTA",
                        principalColumn: "ID_TIPO_CONTA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_CLIENTE_ID_BANCO",
                table: "CONTA_CLIENTE",
                column: "ID_BANCO");

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_CLIENTE_ID_CLIENTE",
                table: "CONTA_CLIENTE",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_CLIENTE_ID_FORMA_RECEBIMENTO",
                table: "CONTA_CLIENTE",
                column: "ID_FORMA_RECEBIMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_CLIENTE_ID_TIPO_CONTA",
                table: "CONTA_CLIENTE",
                column: "ID_TIPO_CONTA");

            migrationBuilder.AddForeignKey(
                name: "FK_RENDIMENTO_CLIENTE_CONTA_CLIENTE_ID_CONTA_CLIENTE",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_CONTA_CLIENTE",
                principalTable: "CONTA_CLIENTE",
                principalColumn: "ID_CONTA_CLIENTE");

            migrationBuilder.AddForeignKey(
                name: "FK_RENDIMENTO_CLIENTE_CONTA_CLIENTE_ID_CONTA_CLIENTE_RECEBIMENTO",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_CONTA_CLIENTE_RECEBIMENTO",
                principalTable: "CONTA_CLIENTE",
                principalColumn: "ID_CONTA_CLIENTE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RENDIMENTO_CLIENTE_CONTA_CLIENTE_ID_CONTA_CLIENTE",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_RENDIMENTO_CLIENTE_CONTA_CLIENTE_ID_CONTA_CLIENTE_RECEBIMENTO",
                table: "RENDIMENTO_CLIENTE");

            migrationBuilder.DropTable(
                name: "CONTA_CLIENTE");

            migrationBuilder.DropTable(
                name: "FORMA_RECEBIMENTO");

            migrationBuilder.RenameColumn(
                name: "ID_CONTA_CLIENTE_RECEBIMENTO",
                table: "RENDIMENTO_CLIENTE",
                newName: "ID_TIPO_CONTA");

            migrationBuilder.RenameColumn(
                name: "ID_CONTA_CLIENTE",
                table: "RENDIMENTO_CLIENTE",
                newName: "ID_BANCO");

            migrationBuilder.RenameIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_CONTA_CLIENTE_RECEBIMENTO",
                table: "RENDIMENTO_CLIENTE",
                newName: "IX_RENDIMENTO_CLIENTE_ID_TIPO_CONTA");

            migrationBuilder.RenameIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_CONTA_CLIENTE",
                table: "RENDIMENTO_CLIENTE",
                newName: "IX_RENDIMENTO_CLIENTE_ID_BANCO");

            migrationBuilder.AlterColumn<int>(
                name: "ID_TIPO_CONTA",
                table: "TIPO_CONTA",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "AGENCIA",
                table: "RENDIMENTO_CLIENTE",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CONTA",
                table: "RENDIMENTO_CLIENTE",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RENDIMENTO_CLIENTE_BANCO_ID_BANCO",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_BANCO",
                principalTable: "BANCO",
                principalColumn: "ID_BANCO");

            migrationBuilder.AddForeignKey(
                name: "FK_RENDIMENTO_CLIENTE_TIPO_CONTA_ID_TIPO_CONTA",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_TIPO_CONTA",
                principalTable: "TIPO_CONTA",
                principalColumn: "ID_TIPO_CONTA");
        }
    }
}

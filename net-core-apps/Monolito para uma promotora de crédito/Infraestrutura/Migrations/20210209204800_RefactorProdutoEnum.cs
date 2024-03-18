using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class RefactorProdutoEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_PRODUTO_ID_PRODUTO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_LEAD_PRODUTO_ID_PRODUTO",
                table: "LEAD");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTO_INTENCAO_OPERACAO_PASSO_PRODUTO_ID_PRODUTO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO");

            migrationBuilder.AddColumn<int>(name: "ID_TEMP_PRODUTO", table: "PRODUTO", defaultValue: 0);
            migrationBuilder.Sql(@"UPDATE dbo.PRODUTO SET ID_TEMP_PRODUTO = ID_PRODUTO");
            migrationBuilder.DropPrimaryKey(name: "PK_PRODUTO", table: "PRODUTO");
            migrationBuilder.DropColumn(name: "ID_PRODUTO", table: "PRODUTO");
            migrationBuilder.RenameColumn(name: "ID_TEMP_PRODUTO", table: "PRODUTO", "ID_PRODUTO");
            migrationBuilder.AddPrimaryKey(name: "PK_PRODUTO", "PRODUTO", column: "ID_PRODUTO");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_PRODUTO_ID_PRODUTO",
                table: "INTENCAO_OPERACAO",
                column: "ID_PRODUTO",
                principalTable: "PRODUTO",
                principalColumn: "ID_PRODUTO");

            migrationBuilder.AddForeignKey(
                name: "FK_LEAD_PRODUTO_ID_PRODUTO",
                table: "LEAD",
                column: "ID_PRODUTO",
                principalTable: "PRODUTO",
                principalColumn: "ID_PRODUTO");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTO_INTENCAO_OPERACAO_PASSO_PRODUTO_ID_PRODUTO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                column: "ID_PRODUTO",
                principalTable: "PRODUTO",
                principalColumn: "ID_PRODUTO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID_PRODUTO",
                table: "PRODUTO",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}

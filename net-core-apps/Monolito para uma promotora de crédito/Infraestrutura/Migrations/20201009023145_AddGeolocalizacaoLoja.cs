using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Repositorio.Migrations
{
    public partial class AddGeolocalizacaoLoja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "GEOLOCALIZACAO",
                table: "LOJA",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GEOLOCALIZACAO",
                table: "LOJA");
        }
    }
}

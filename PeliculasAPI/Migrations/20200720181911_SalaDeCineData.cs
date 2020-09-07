using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace PeliculasAPI.Migrations
{
    public partial class SalaDeCineData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SalasDeCines",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[] { 2, "Cinemark San Miguel", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-77.088628 -12.076616)") });

            migrationBuilder.InsertData(
                table: "SalasDeCines",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[] { 3, "Cineplanet San Miguel", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-77.083678 -12.075751)") });

            migrationBuilder.InsertData(
                table: "SalasDeCines",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[] { 4, "Cineplanet Larcomar", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-77.031 -12.131549)") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SalasDeCines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SalasDeCines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SalasDeCines",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}

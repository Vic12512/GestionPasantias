using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestionPasantias.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedEstadosPostulacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EstadosPostulacion",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "PendienteTutor" },
                    { 2, "AprobadaTutor" },
                    { 3, "RechazadaTutor" },
                    { 4, "AprobadaSupervisor" },
                    { 5, "RechazadaSupervisor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstadosPostulacion",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EstadosPostulacion",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EstadosPostulacion",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EstadosPostulacion",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EstadosPostulacion",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

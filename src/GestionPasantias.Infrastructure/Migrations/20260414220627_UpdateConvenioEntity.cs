using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPasantias.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConvenioEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Detalles",
                table: "Convenios",
                newName: "NombreUniversidad");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "Convenios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "Convenios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NombreEmpresa",
                table: "Convenios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreEstudiante",
                table: "Convenios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "Convenios");

            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "Convenios");

            migrationBuilder.DropColumn(
                name: "NombreEmpresa",
                table: "Convenios");

            migrationBuilder.DropColumn(
                name: "NombreEstudiante",
                table: "Convenios");

            migrationBuilder.RenameColumn(
                name: "NombreUniversidad",
                table: "Convenios",
                newName: "Detalles");
        }
    }
}

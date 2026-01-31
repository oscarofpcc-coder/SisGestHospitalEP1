using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SisGestionCitasMedicas.Migrations
{
    /// <inheritdoc />
    public partial class InicioCreacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctores",
                columns: table => new
                {
                    doctor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    especialidad = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctores", x => x.doctor_id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    paciente_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.paciente_id);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    cita_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paciente_id = table.Column<int>(type: "int", nullable: false),
                    doctor_id = table.Column<int>(type: "int", nullable: false),
                    fecha_hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    motivo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.cita_id);
                    table.ForeignKey(
                        name: "FK_Citas_Doctores_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctores",
                        principalColumn: "doctor_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Pacientes_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "Pacientes",
                        principalColumn: "paciente_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_doctor_id",
                table: "Citas",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_paciente_id",
                table: "Citas",
                column: "paciente_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Doctores");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}

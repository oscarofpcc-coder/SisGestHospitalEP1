using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisGestionCitasMedicas.Models
{
    [Table("Citas")]
    public class Cita
    {
        [Key]
        [Column("cita_id")]
        public int CitaId { get; set; }

        [Required]
        [Column("paciente_id")]
        public int PacienteId { get; set; }

        [Required]
        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Required]
        [Column("fecha_hora")]
        public DateTime FechaHora { get; set; }

        [StringLength(250)]
        [Column("motivo")]
        public string? Motivo { get; set; }

        [Required, StringLength(30)]
        [Column("estado")]
        public string Estado { get; set; } = "Programada";
        // Programada | Atendida | Cancelada

        // Navigation
        public Paciente? Paciente { get; set; }
        public Doctor? Doctor { get; set; }
    }
}


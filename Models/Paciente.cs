using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisGestionCitasMedicas.Models
{
    [Table("Pacientes")]
    public class Paciente
    {
        [Key]
        [Column("paciente_id")]
        public int PacienteId { get; set; }

        [Required, StringLength(80)]
        [Column("nombre")]
        public string Nombre { get; set; } = "";

        [Required, StringLength(80)]
        [Column("apellido")]
        public string Apellido { get; set; } = "";

        [DataType(DataType.Date)]
        [Column("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(20)]
        [Column("telefono")]
        public string? Telefono { get; set; }


        // FK
        [Column("empresa_id")]
        public int EmpresaId { get; set; } = 1;
        public Empresa? Empresas { get; set; } = null!;  // Navegación

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}

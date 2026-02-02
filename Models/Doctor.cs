using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisGestionCitasMedicas.Models
{
    [Table("Doctores")]
    public class Doctor
    {
        [Key]
        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Required, StringLength(80)]
        [Column("nombre")]
        public string Nombre { get; set; } = "";

        [Required, StringLength(80)]
        [Column("apellido")]
        public string Apellido { get; set; } = "";

        [Required, StringLength(120)]
        [Column("especialidad")]
        public string Especialidad { get; set; } = "";

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


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisGestionCitasMedicas.Models
{
    [Table("Empresas")]
    public class Empresa
    {
        [Key]
        [Column("empresa_id")]
        public int EmpresaId { get; set; }

        [Required, StringLength(20)]
        [Column("ced_ruc")]
        public string CedRuc { get; set; } = "";

        [Required, StringLength(100)]
        [Column("razon_social")]
        public string RazonSocial { get; set; } = "";

        [StringLength(100)]
        [Column("nom_tit")]
        public string? NombreComercial { get; set; }

        
        [Column("obligado_contabilidad")]
        public bool? ObligadoContabilidad { get; set; }

        [Column("fec_doc")]
        public string? FechaDoc { get; set; }

        [Column("estado")]
        public string? Estado { get; set; }

        // Navegación (1 a muchos)        
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
        public ICollection<Doctor> Doctores { get; set; } = new List<Doctor>();
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
     }

}


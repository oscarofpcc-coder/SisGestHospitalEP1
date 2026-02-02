using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisGestionCitasMedicas.Models
{
    [Table("Tablas")]
    public class Tabla
    {
        [Key]
        [Column("tabla_id")]
        public int TablaId { get; set; }

        [Required, StringLength(20)]
        [Column("cod_tabla")]
        public string CodTabla { get; set; } = "";

        [Required, StringLength(20)]
        [Column("descripcion")]
        public string Descripcion { get; set; } = "";

        [Required, StringLength(1)]
        [Column("estado")]
        public string Estado { get; set; } = "";

        // 🔁 Navegación: 1 Tabla → muchos Catalogos
        public ICollection<Catalogo> Catalogos { get; set; }= new List<Catalogo>();
    }
}

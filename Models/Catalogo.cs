using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisGestionCitasMedicas.Models
{
    [Table("Catalogos")]
    public class Catalogo
    {
        [Key]
        [Column("catalogo_id")]
        public int Id { get; set; }

        [Required, StringLength(20)]
        [Column("cod_catalogo")]
        public string CodCatalogo { get; set; } = "";


        [Required, StringLength(20)]
        [Column("descripcion")]
        public string Descripcion { get; set; } = "";

        [Required, StringLength(1)]
        [Column("estado")]
        public string Estado { get; set; } = "";

        // FK
        [Column("tabla_id")]
        public int TablaId { get; set; }

        // Navegacion
        public Tabla? Tabla { get; set; } = null!;
    }
}

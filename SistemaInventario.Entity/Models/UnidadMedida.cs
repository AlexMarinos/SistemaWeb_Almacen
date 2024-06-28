using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class UnidadMedida
    {
        public UnidadMedida()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int IdUnidadMedida { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool? EsActivo { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}

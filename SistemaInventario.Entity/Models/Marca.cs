using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int IdMarca { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool? EsActivo { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}

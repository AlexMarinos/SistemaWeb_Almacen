using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class Subcategoria
    {
        public Subcategoria()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int IdSubcategoria { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdCategoria { get; set; }
        public bool? EsActivo { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}

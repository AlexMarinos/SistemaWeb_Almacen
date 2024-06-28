using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class Articulo
    {
        public Articulo()
        {
            DetalleIngresos = new HashSet<DetalleIngreso>();
            DetalleSalida = new HashSet<DetalleSalida>();
        }

        public int IdArticulo { get; set; }
        public string CodigoArticulo { get; set; }
        public string Serie { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Stock { get; set; }
 
        public int IdMarca { get; set; }
        public int IdUnidadMedida { get; set; }
        public int IdSubcategoria { get; set; }
        public bool? EsActivo { get; set; }

        public virtual Marca IdMarcaNavigation { get; set; } = null!;
        public virtual Subcategoria IdSubcategoriaNavigation { get; set; } = null!;
        public virtual UnidadMedida IdUnidadMedidaNavigation { get; set; } = null!;
        public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; }
        public virtual ICollection<DetalleSalida> DetalleSalida { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class DetalleIngreso
    {
        public int IdDetalleIngreso { get; set; }
        public int IdIngreso { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime FechaIngreso { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
        public virtual Ingreso IdIngresoNavigation { get; set; } = null!;
    }
}

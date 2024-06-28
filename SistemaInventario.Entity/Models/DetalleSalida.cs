using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class DetalleSalida
    {
        public int IdDetalleSalida { get; set; }
        public int IdSalida { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public bool? EsActivo { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
        public virtual Salida IdSalidaNavigation { get; set; } = null!;
    }
}

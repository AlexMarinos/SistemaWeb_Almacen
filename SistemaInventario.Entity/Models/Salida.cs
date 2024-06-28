using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class Salida
    {
        public Salida()
        {
            DetalleSalida = new HashSet<DetalleSalida>();
        }

        public int IdSalida { get; set; }
        public int IdPersonal { get; set; }
        public DateTime FechaSalida { get; set; }
        public string NumeroPedido { get; set; } = null!;
        public string Recepcion { get; set; } = null!;
        public bool? EsActivo { get; set; }

        public virtual Personal IdPersonalNavigation { get; set; } = null!;
        public virtual ICollection<DetalleSalida> DetalleSalida { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class Origen
    {
        public Origen()
        {
            Ingresos = new HashSet<Ingreso>();
        }

        public int IdOrigen { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool? EsActivo { get; set; }

        public virtual ICollection<Ingreso> Ingresos { get; set; }
    }
}

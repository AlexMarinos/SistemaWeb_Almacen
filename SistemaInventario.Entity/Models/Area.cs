using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class Area
    {
        public Area()
        {
     
            Servicios = new HashSet<Servicio>();
        }

        public int IdArea { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool? EsActivo { get; set; }

        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}

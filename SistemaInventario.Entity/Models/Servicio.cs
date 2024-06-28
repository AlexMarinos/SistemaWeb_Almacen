using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            Personals = new HashSet<Personal>();
        }

        public int IdServicio { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdArea { get; set; }
        public bool? EsActivo { get; set; }

        public virtual Area IdAreaNavigation { get; set; } = null!;
        public virtual ICollection<Personal> Personals { get; set; }
    }
}

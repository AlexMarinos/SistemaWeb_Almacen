using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class Personal
    {
        public Personal()
        {
            Ingresos = new HashSet<Ingreso>();
            Salida = new HashSet<Salida>();
        }

        public int IdPersonal { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string Condicion { get; set; } = null!;
        public int IdServicio { get; set; }
        public bool? EsActivo { get; set; }

        public virtual Servicio IdServicioNavigation { get; set; } = null!;
        public virtual ICollection<Ingreso> Ingresos { get; set; }
        public virtual ICollection<Salida> Salida { get; set; }
    }
}

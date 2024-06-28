using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class Ingreso
    {
        public Ingreso()
        {
            DetalleIngresos = new HashSet<DetalleIngreso>();
        }

        public int IdIngreso { get; set; }
        public DateOnly FechaRegistro { get; set; }
        public string NumeroPecosa { get; set; } = null!;
        public int IdOrigen { get; set; }
        public int IdPersonal { get; set; }
        public string Justificacion { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }

        public virtual Origen IdOrigenNavigation { get; set; } = null!;
        public virtual Personal IdPersonalNavigation { get; set; } = null!;
        public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SistemaInventario.Entity.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Subcategoria = new HashSet<Subcategoria>();
        }

        public int IdCategoria { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool? EsActivo { get; set; }

        public virtual ICollection<Subcategoria> Subcategoria { get; set; }
    }
}

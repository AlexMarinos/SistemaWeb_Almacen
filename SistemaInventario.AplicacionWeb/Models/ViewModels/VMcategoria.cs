using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Models.ViewModels
{
    public class VMcategoria
    {
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public int EsActivo { get; set; }
    }

}

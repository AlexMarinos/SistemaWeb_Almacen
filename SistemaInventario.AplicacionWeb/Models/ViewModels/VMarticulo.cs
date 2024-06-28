using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Models.ViewModels
{
    public class VMarticulo
    {
        public int IdArticulo { get; set; }
        public string CodigoArticulo { get; set; }
        public string Serie { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Stock { get; set; }
        //public DateOnly FechaVencimiento { get; set; }
        public int IdMarca { get; set; }
        public string MarcaDescripcion { get; set; }
        public int IdUnidadMedida { get; set; }     
        public string UnidadMedidaDescripcion { get; set; }
        public int IdSubcategoria { get; set; }
        public string SubcategoriaDescripcion { get; set; }
        public int EsActivo { get; set; }



    }
}

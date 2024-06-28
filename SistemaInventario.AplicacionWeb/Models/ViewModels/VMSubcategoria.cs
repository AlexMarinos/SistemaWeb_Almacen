namespace SistemaInventario.AplicacionWeb.Models.ViewModels
{
    public class VMSubcategoria
    {
        public int IdSubCategoria { get; set; }
        public string Descripcion { get; set; }
        public int Idcategoria { get; set; }
        public string categoriaDescripcion { get; set; }
        public int esActivo { get; set; }
    }
}

namespace SistemaInventario.AplicacionWeb.Models.ViewModels
{
    public class VMservicio
    {
        public int IdServicio { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdArea { get; set; }
        public string Descripcionarea { get; set; } = null!;
        public int  EsActivo { get; set; }

    }
}

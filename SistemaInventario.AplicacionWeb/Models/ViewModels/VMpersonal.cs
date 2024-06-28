namespace SistemaInventario.AplicacionWeb.Models.ViewModels
{
    public class VMpersonal
    {
        public int IdPersonal { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Dni { get; set; }
        public string? Celular { get; set; }
        public string? Condicion { get; set; }
   
        public int IdServicio { get; set; }
        public string ServicioDescripcion { get; set; }
        //public DateTime? FechaRegistro { get; set; }
        public int EsActivo { get; set; }
    }
}

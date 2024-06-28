namespace SistemaInventario.AplicacionWeb.Models.ViewModels
{
    public class VMsalida
    {
        public int IdSalida { get; set; }
        public int IdPersonal { get; set; }
        public string personalDescripcion { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string? NumeroPedido { get; set; }
        public string? Recepcion { get; set; }
        public bool? Estado { get; set; }
        //public DateTime? FechaRegistro { get; set; }

    }
}

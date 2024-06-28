using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Interfaces
{
    public interface IsalidaService
    {
        Task<List<Articulo>> Obtenerproducto(string busqueda);
        Task<Salida>Registrar(Salida entidad);

        Task<List<Salida>> Historial(string numeropedido ,string fechainicio, string fechafin);

        Task<Salida> Detalle(string numerosalida);//agregar el id

        Task<List<DetalleSalida>> Reporte(string fechainicio, string fechafin);
    }
}

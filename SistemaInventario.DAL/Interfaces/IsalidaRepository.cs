using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.DAL.Interfaces
{
     public   interface IsalidaRepository :IGenericReposity<Salida>
    {
        Task<Salida> Registrar(Salida entidad);
        Task<List<DetalleSalida>> Reporte(DateTime FechaInicio , DateTime FechaFin);

    }
}

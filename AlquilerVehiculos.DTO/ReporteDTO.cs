using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.DTO
{
    public class ReporteDTO
    {
        

        public string? NumeroDocumento {  get; set; }
        public string? TipoPago { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public string? TotalVenta { get; set; }
        public string? Vehiculo { get; set; }
        public int? Cantidad { get; set; }
        public string? Precio { get; set; }
        public string? Total {  get; set; }
    }
}

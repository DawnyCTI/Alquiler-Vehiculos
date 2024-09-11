using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.DTO
{
    public class VehiculoDTO
    {
        public int IdVehiculo { get; set; }

        public string? Nombre { get; set; }

        public int? IdCategoria { get; set; }

        public int? DescripcionCategoria { get; set; }

        public int? Stock { get; set; }

        public string? PrecioAlquiler { get; set; }

        public int? EsActivo { get; set; }
    }
}

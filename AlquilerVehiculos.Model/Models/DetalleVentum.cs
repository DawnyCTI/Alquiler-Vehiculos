using System;
using System.Collections.Generic;

namespace AlquilerVehiculos.Model.Models;

public partial class DetalleVentum
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int? IdVehiculo { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public virtual Vehiculo? IdVehiculoNavigation { get; set; }

    public virtual Ventum? IdVentaNavigation { get; set; }
}

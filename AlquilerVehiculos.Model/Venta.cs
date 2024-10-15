using System;
using System.Collections.Generic;

namespace AlquilerVehiculos.Model;

public partial class Venta
{
    public readonly object IdVehiculoNavigation;

    public int IdVenta { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? TipoPago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; } = new List<DetalleVenta>();
    public object IdVentaNavigation { get; set; }
    public DetalleVenta IdDetalleVentaNavigation { get; set; }
}

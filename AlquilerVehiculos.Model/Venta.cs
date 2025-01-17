﻿using System;
using System.Collections.Generic;

namespace AlquilerVehiculos.Model;

public partial class Venta
{
    public int IdVenta { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? TipoPago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    // Relación con DetalleVenta
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    
    public virtual Vehiculo IdVehiculoNavigation { get; set; }
}

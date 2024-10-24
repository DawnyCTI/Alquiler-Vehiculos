using System;
using System.Collections.Generic;

namespace AlquilerVehiculos.Model.Models;

public partial class Vehiculo
{
    public int IdVehiculo { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? IdCategoria { get; set; }

    public int? Stock { get; set; }

    public decimal? PrecioAlquiler { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? ImagenUrl { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; } = new List<DetalleVentum>();

    public virtual Categorium? IdCategoriaNavigation { get; set; }
}

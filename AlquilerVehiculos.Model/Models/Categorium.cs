﻿using System;
using System.Collections.Generic;

namespace AlquilerVehiculos.Model.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();
}

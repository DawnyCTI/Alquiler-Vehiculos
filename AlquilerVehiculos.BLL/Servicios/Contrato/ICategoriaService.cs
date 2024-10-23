﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlquilerVehiculos.DTO;

namespace AlquilerVehiculos.BLL.Servicios.Contrato
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>>Lista();
    }
}

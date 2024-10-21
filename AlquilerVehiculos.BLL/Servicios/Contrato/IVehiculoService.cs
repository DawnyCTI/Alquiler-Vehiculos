using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlquilerVehiculos.DTO;

namespace AlquilerVehiculos.BLL.Servicios.Contrato
{
    public interface IVehiculoService
    {
        Task<List<VehiculoDTO>> Lista();
        Task<VehiculoDTO> Crear(VehiculoDTO modelo);
        Task<VehiculoDTO> Editar(VehiculoDTO modelo);
        Task<VehiculoDTO> Eliminar(int id);

    }
}

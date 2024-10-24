using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AlquilerVehiculos.BLL.Servicios.Contrato;
using AlquilerVehiculos.DTO;
using AlquilerVehiculos.API.Utilidad;

namespace AlquilerVehiculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoServicio;

        public VehiculoController(IVehiculoService vehiculoServicio)
        {
            _vehiculoServicio = vehiculoServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<VehiculoDTO>>();

            try
            {
                rsp.status = true;
                rsp.Value = await _vehiculoServicio.Lista();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] VehiculoDTO vehiculo)
        {
            var rsp = new Response<VehiculoDTO>();

            try
            {
                rsp.status = true;
                rsp.Value = await _vehiculoServicio.Crear(vehiculo);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] VehiculoDTO vehiculo)
        {
            var rsp = new Response<VehiculoDTO>();

            try
            {
                rsp.status = true;
                rsp.Value = await _vehiculoServicio.Editar(vehiculo);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                await _vehiculoServicio.Eliminar(id);
                rsp.Value = true;
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                rsp.Value = false;
            }

            return Ok(rsp);
        }
    }
}

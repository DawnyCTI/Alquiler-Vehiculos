using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AlquilerVehiculos.BLL.Servicios.Contrato;
using AlquilerVehiculos.DTO;
using AlquilerVehiculos.API.Utilidad;

namespace AlquilerVehiculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolServicio;

        public RolController(IRolService rolServicio)
        {
            _rolServicio = rolServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<RolDTO>>();

            try
            {
                rsp.status = true;
                rsp.Value = await _rolServicio.Lista();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }
    }

}

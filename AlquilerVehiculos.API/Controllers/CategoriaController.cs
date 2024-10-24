using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // Add this line

using AlquilerVehiculos.BLL.Servicios.Contrato;
using AlquilerVehiculos.DTO;
using AlquilerVehiculos.API.Utilidad;

namespace AlquilerVehiculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaServicio;
        private readonly IConfiguration _configuration; // Add this line

        public CategoriaController(ICategoriaService categoriaServicio, IConfiguration configuration) // Modify constructor
        {
            _categoriaServicio = categoriaServicio;
            _configuration = configuration; // Add this line
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CategoriaDTO>>();

            try
            {
                rsp.status = true;
                rsp.Value = await _categoriaServicio.Lista();
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


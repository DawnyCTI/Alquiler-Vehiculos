using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AlquilerVehiculos.BLL.Servicios.Contrato;
using AlquilerVehiculos.DTO;
using AlquilerVehiculos.API.Utilidad;

namespace AlquilerVehiculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServicio;

        public UsuarioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<UsuarioDTO>>();

            try
            {
                rsp.status = true;
                rsp.Value = await _usuarioServicio.Lista();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login)
        {
            var rsp = new Response<SesionDTO>();

            try
            {
                rsp.status = true;
                rsp.Value = await _usuarioServicio.ValidarCredenciales(login.Correo,login.Clave);
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
        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new Response<UsuarioDTO>();

            try
            {
                rsp.status = true;
                // Llamada al servicio para crear un nuevo usuario
                var sesion = await _usuarioServicio.Crear(usuario);
                // Asignación de valores al objeto de respuesta | Si no funciona se debe cambiar esta implementación
                rsp.Value = new UsuarioDTO
                {
                    IdUsuario = sesion.IdUsuario,
                    NombreCompleto = sesion.NombreCompleto,
                    Correo = sesion.Correo,
                    RolDescripcion = sesion.RolDescripcion
                };
            }
            catch (Exception ex)
            {
                // Manejo de excepciones y asignación del mensaje de error
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            // Retorno de la respuesta en formato JSON
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new Response<bool>();

           try
            {
                rsp.status = true;
                rsp.Value = await _usuarioServicio.Editar(usuario);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.Value = await _usuarioServicio.Eliminar(id);
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

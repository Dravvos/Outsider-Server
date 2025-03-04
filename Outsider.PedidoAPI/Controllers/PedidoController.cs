using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Outsider.PedidoAPI.Services;
using Outsider.PedidoAPI.Utils;

namespace Outsider.PedidoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {

        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet("{usuarioId}"), Authorize(Roles = Role.Cliente)]
        public async Task<IActionResult> GetPedidosByUser(Guid usuarioId)
        {
            try
            {
                var pedidos = await _service.GetByUser(usuarioId);
                if(pedidos==null || pedidos.Any() == false) return NotFound();

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Outsider.Pagamentos;
using StackExchange.Redis;
using System.Security.Cryptography;


namespace Outsider.PagamentoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redisCon;

        public PagamentoController(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
        }

        [HttpGet("{carrinhoId}"), Authorize(Roles = Utils.Role.Cliente)]
        public async Task<IActionResult> GetStatusPagamento(Guid carrinhoId)
        {
            try
            {
                var db = _redisCon.GetDatabase();
                var clientSecret = (await db.StringGetAsync("Pagamento:" + carrinhoId)).ToString();
                if (string.IsNullOrEmpty(clientSecret))
                    return NotFound();

                return Ok(clientSecret);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{carrinhoId}"), Authorize(Roles = Utils.Role.Cliente)]
        public async Task<IActionResult> Process(Guid carrinhoId,[FromBody] float Valor)
        {
            try
            {
                var db = _redisCon.GetDatabase();
                var secret = (await db.StringGetAsync("Pagamento:" + carrinhoId)).ToString();
                if (!string.IsNullOrEmpty(secret))
                    return Ok(secret);

                var clientSecret = new ProcessoPagamento().CriarPagamento(Valor);
                
                await db.StringSetAsync($"Pagamento:{carrinhoId}", clientSecret);
                return Ok(clientSecret);
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
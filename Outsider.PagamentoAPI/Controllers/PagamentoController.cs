using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Outsider.DTO;
using Outsider.Pagamentos;
using StackExchange.Redis;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text.Json;


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
        public async Task<IActionResult> Process(Guid carrinhoId, [FromBody] float Valor)
        {
            try
            {
                var db = _redisCon.GetDatabase();
                var secret = (await db.StringGetAsync("Pagamento:" + carrinhoId)).ToString();
                if (!string.IsNullOrEmpty(secret))
                    return Ok(secret);

                var _client = new HttpClient();
                string? token = await HttpContext.GetTokenAsync("access_token");
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _client.GetAsync($"/api/Carrinho/GetById/{carrinhoId}");
                var content = await response.Content.ReadAsStringAsync();

                var carrinho = JsonSerializer.Deserialize<List<ItemCarrinhoDTO>>(content,
                    new JsonSerializerOptions
                    { WriteIndented = true, PropertyNameCaseInsensitive = true });

                if (carrinho == null || carrinho.Count == 0)
                    return NotFound("Carrinho não encontrado");

                if (Valor <= 20)
                {
                    Valor = carrinho.Sum(x => x.Produto.Preco);
                }
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
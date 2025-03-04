using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Outsider.CarrinhoAPI.Mensagens;
using Outsider.CarrinhoAPI.MessageConsumer;
using Outsider.CarrinhoAPI.Model;
using Outsider.CarrinhoAPI.RabbitMQSender;
using Outsider.CarrinhoAPI.Service;

namespace Outsider.CarrinhoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICupomService _cupomService;
        private readonly IConfiguration _configuration;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public CarrinhoController(ICarrinhoService carrinhoRepository, ICupomService cupomService, IConfiguration configuration, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _carrinhoService = carrinhoRepository ?? throw new ArgumentNullException(nameof(carrinhoRepository));
            _cupomService = cupomService ?? throw new ArgumentNullException(nameof(cupomService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
        }

        [HttpGet("[action]/{userId}"), Authorize]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            try
            {
                var carrinho = await _carrinhoService.GetCarrinhoPorUsuario(userId);
                if (carrinho == null || carrinho.Any() == false)
                    return NotFound();

                return Ok(carrinho);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AdicionarAoCarrinho([FromBody] CarrinhoItem item)
        {
            try
            {
                await _carrinhoService.AdicionarItemAoCarrinho(item.UsuarioId, item.ProdutoId, item.Quantidade);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> AtualizarCarrinho([FromBody] CarrinhoItem item)
        {
            try
            {
                await _carrinhoService.AdicionarItemAoCarrinho(item.UsuarioId, item.ProdutoId, item.Quantidade);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{itemId}"), Authorize]
        public async Task<IActionResult> RemoverDoCarrinho(Guid itemId)
        {
            try
            {
                await _carrinhoService.RemoverItemDoCarrinho(itemId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("LimparCarrinho/{usuarioId}"), Authorize]
        public async Task<IActionResult> LimparCarrinho(Guid usuarioId)
        {
            try
            {
                await _carrinhoService.LimparCarrinho(usuarioId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [Authorize, HttpPost("[action]/{usuarioId}/{cupom}")]
        public async Task<IActionResult> AplicarCupom(Guid usuarioId, string cupom)
        {
            try
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                await _carrinhoService.InserirCupom(usuarioId, cupom, token);
                return Ok();

            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [Authorize, HttpDelete("[action]/{usuarioId}")]
        public async Task<IActionResult> RemoverCupom(Guid usuarioId)
        {
            try
            {
                await _carrinhoService.RemoverCupom(usuarioId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]"), Authorize]
        public async Task<IActionResult> Checkout([FromBody] CheckoutHeaderMessage dto)
        {
            if (dto == null) return BadRequest(ModelState);
            var itensCarrinho = await _carrinhoService.GetCarrinhoPorUsuario(dto.UsuarioId);
            if (itensCarrinho == null || itensCarrinho.Any() == false) return NotFound();
            string token = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(dto.CodigoCupom) == false)
            {
                var cupom = await _cupomService.GetCupom(dto.CodigoCupom, token);
                if (((float)cupom.PorcentagemDesconto / 100 * (dto.ValorDesconto + dto.ValorCompra)).ToString("0.00") != dto.ValorDesconto.ToString("0.00"))
                {
                    return StatusCode(412, "Cupom mudou de desconto, favor confirmar!");
                }

            }

            dto.ItensCarrinho = itensCarrinho;

            //RABBIT MQ
            _rabbitMQMessageSender.SendMessage(dto, "checkoutFila", _configuration);

            await _carrinhoService.LimparCarrinho(dto.UsuarioId);
            return Ok(dto);
        }

    }
}

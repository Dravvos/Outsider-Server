using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Outsider.DTO;
using Outsider.ProdutoAPI.Mensagens;
using Outsider.ProdutoAPI.RabbitMQSender;
using Outsider.ProdutoAPI.Services;
using Outsider.ProdutoAPI.Utils;
using System.Text;
using System.Text.Json;

namespace Outsider.ProdutoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IProdutoService _Service;
        private IRabbitMQMessageSender _rabbitMQMessageSender;



        public ProdutoController(IProdutoService Service, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _Service = Service ?? throw new
                ArgumentNullException(nameof(Service));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> FindAll()
        {
            try
            {
                var products = await _Service.GetAllClient();
                if (products == null || products.Any() == false) return NotFound();
                return Ok(products);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAll"), Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAllAdmin()
        {
            try
            {
                var products = await _Service.GetAllAdmin();
                if (products == null || products.Any() == false) return NotFound();
                return Ok(products);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProdutoDTO>> FindById(Guid id)
        {
            try
            {
                var product = await _Service.GetById(id);
                if (product == null) return NotFound();
                return Ok(product);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{categoriaId}/{corId}/{nome}")]
        [Authorize]
        public async Task<ActionResult<ProdutoDTO>> FindProdutosEmEstoque(Guid categoriaId, Guid corId, string nome)
        {
            try
            {
                var products = await _Service.GetAllEmEstoque(categoriaId, corId, nome);
                if (products == null || products.Any() == false) return NotFound();
                return Ok(products);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProdutoDTO>> Create([FromBody] ProdutoDTO dto)
        {
            if (dto == null) return BadRequest();
            try
            {
                await _Service.Create(dto);

                dto = await _Service.GetById(dto.Id.Value);
                var json = JsonSerializer.Serialize(dto);
                var message = JsonSerializer.Deserialize<ProdutoMessage>(json);

                _rabbitMQMessageSender.SendMessage(message);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                if (ex.InnerException == null)
                    return BadRequest(ex.Message);

                return BadRequest(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return StatusCode(500, ex.Message);
                else
                    return StatusCode(500, ex.InnerException.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProdutoDTO>> Update(Guid id, [FromBody] ProdutoDTO dto)
        {
            if (dto == null) return BadRequest();
            if (dto.Id != id) return BadRequest("Id da requisição é diferente do id do Body");
            try
            {
                await _Service.Update(dto);

                var json = JsonSerializer.Serialize(dto);
                var message = JsonSerializer.Deserialize<ProdutoMessage>(json);
                _rabbitMQMessageSender.SendMessage(message);

                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                if (ex.InnerException == null)
                    return BadRequest(ex.Message);
                else
                    return BadRequest(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return StatusCode(500, ex.Message);
                else
                    return StatusCode(500, ex.InnerException.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _Service.Delete(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return StatusCode(500, ex.Message);
                else
                    return StatusCode(500, ex.InnerException.Message);
            }
        }
    }
}

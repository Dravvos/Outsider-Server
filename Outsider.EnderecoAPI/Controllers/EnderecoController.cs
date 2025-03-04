using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Outsider.DTO;
using Outsider.EnderecoAPI.Mensagens;
using Outsider.EnderecoAPI.Migrations;
using Outsider.EnderecoAPI.RabbitMQSender;
using Outsider.EnderecoAPI.Services;
using Outsider.EnderecoAPI.Utils;
using System.Text.Json;

namespace Outsider.EnderecoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _service;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        public EnderecoController(IEnderecoService service, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _service = service;
        }

        [HttpGet("{usuarioId}"), Authorize(Roles =Role.Cliente)]
        public async Task<IActionResult> GetAll(Guid usuarioId)
        {
            try
            {
                var enderecos = await _service.GetAll(usuarioId);
                if (enderecos == null || enderecos.Any() == false) return NotFound();
                return Ok(enderecos);
            }
            catch (Exception ex) 
            {
                if (ex.InnerException == null)
                    return StatusCode(500, ex.Message);

                return StatusCode(500, ex.InnerException.Message);
            }
        }
        
        [HttpGet("[action]/{id}"), Authorize(Roles =Role.Cliente)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var endereco = await _service.GetById(id);
                if (endereco == null) return NotFound();
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return StatusCode(500, ex.Message);

                return StatusCode(500, ex.InnerException.Message);
            }
        }

        [HttpPost,Authorize(Roles =Role.Cliente)]
        public async Task<IActionResult> Create([FromBody] EnderecoDTO dto)
        {
            if (dto == null) return BadRequest(ModelState);
            try
            {
                await _service.AddEndereco(dto);
                dto = await _service.GetById(dto.Id.Value);
                var json = JsonSerializer.Serialize(dto);
                var message = JsonSerializer.Deserialize<EnderecoMessage>(json);
                _rabbitMQMessageSender.SendMessage(message);

                return Ok();
            }
            catch(ArgumentException ex)
            {
                if (ex.InnerException == null)
                    return BadRequest(ex.Message);

                return BadRequest(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return StatusCode(500, ex.Message);

                return StatusCode(500, ex.InnerException.Message);

            }
        }

        [HttpPut("{id}"), Authorize(Roles = Role.Cliente)]
        public async Task<IActionResult> Update(Guid id,[FromBody] EnderecoDTO dto)
        {
            if (dto == null) return BadRequest(ModelState);
            if (dto.Id != id) return BadRequest("O Id da requisição é diferente do Id do Body");

            try
            {
                await _service.UpdateEndereco(dto);
                return Ok();
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
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

                return StatusCode(500, ex.InnerException.Message);

            }
        }

        [HttpDelete("{id}"),Authorize(Roles =Role.Cliente)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteEndereco(id);
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

                return StatusCode(500, ex.InnerException.Message);
            }
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Outsider.DTO;
using Outsider.TabelaGeralAPI.Mensagens;
using Outsider.TabelaGeralAPI.Model;
using Outsider.TabelaGeralAPI.RabbitMQSender;
using Outsider.TabelaGeralAPI.Services;
using Outsider.TabelaGeralAPI.Utils;
using System.Text.Json;

namespace Outsider.TabelaGeralAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabelaGeralController : ControllerBase
    {
        private readonly ITabelaGeralItemService _tabelaGeralItemService;
        private readonly ITabelaGeralService _tabelaGeralService;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public TabelaGeralController(ITabelaGeralItemService tabelaGeralItemService, ITabelaGeralService tabelaGeralService, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _tabelaGeralItemService = tabelaGeralItemService;
            _tabelaGeralService = tabelaGeralService;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        [Route("[action]/{id}")]
        [HttpGet, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> GetTabelaGeralById(Guid id)
        {
            try
            {
                var tabelaGeral = await _tabelaGeralService.GetByIdAsync(id);
                if (tabelaGeral == null) return NotFound();
                return Ok(tabelaGeral);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [Route("[action]/{nome}")]
        [HttpGet, Authorize]
        public async Task<IActionResult> GetTabelaGeralByNome(string nome)
        {
            try
            {
                var tabelaGeral = await _tabelaGeralService.GetByNomeAsync(nome);
                if (tabelaGeral == null) return NotFound();
                return Ok(tabelaGeral);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }


        [Route("[action]")]
        [HttpPost, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> AddTabelaGeral([FromBody] TabelaGeralDTO dto)
        {
            try
            {
                var ret = await _tabelaGeralService.AddAsync(dto);
                return Ok(ret);

            }
            catch (ArgumentException ex) { return BadRequest(ex); }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return StatusCode(500, ex.Message);
                else
                    return StatusCode(500, ex.InnerException.Message);
            }
        }

        [Route("[action]/{tabelaGeralId}")]
        [HttpPut, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> UpdateTabelaGeral(Guid tabelaGeralId, [FromBody] TabelaGeralDTO dto)
        {
            if (tabelaGeralId != dto.Id) return BadRequest("Id da requisição é diferente do Id do Body");
            try
            {
                await _tabelaGeralService.UpdateAsync(dto);
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

        [Route("[action]/{tabelaGeralId}")]
        [HttpDelete, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteTabelaGeral(Guid tabelaGeralId)
        {
            if (await _tabelaGeralService.GetByIdAsync(tabelaGeralId) == null)
                return NotFound();
            try
            {
                await _tabelaGeralService.DeleteAsync(tabelaGeralId);
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

        [Route("[action]/{id}")]
        [HttpGet, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> GetTabelaGeralItemById(Guid id)
        {
            var tabelaGeral = await _tabelaGeralItemService.GetByIdAsync(id);
            if (tabelaGeral == null) return NotFound();
            return Ok(tabelaGeral);
        }

        [Route("[action]")]
        [HttpGet, Authorize]
        public async Task<IActionResult> GetTabelasGeraisItems(Guid? tabelaGeralId = null)
        {
            try
            {
                var tabelasGerais = await _tabelaGeralItemService.GetAllItemsAsync(tabelaGeralId);
                return Ok(tabelasGerais);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> AddTabelaGeralItem([FromBody] TabelaGeralItemDTO dto)
        {
            try
            {
                await _tabelaGeralItemService.AddAsync(dto);

                dto = await _tabelaGeralItemService.GetByIdAsync(dto.Id.Value);
                var json = JsonSerializer.Serialize(dto);
                var message = JsonSerializer.Deserialize<TabelaGeralItemMessage>(json);
                _rabbitMQMessageSender.SendMessage(message);

                return StatusCode(201, dto.Id);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return StatusCode(500, ex.Message);
                else
                    return StatusCode(500, ex.InnerException.Message);
            }

        }

        [Route("[action]/{tabelaGeralItemId}")]
        [HttpPut, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> UpdateTabelaGeralItem(Guid tabelaGeralItemId, [FromBody] TabelaGeralItemDTO dto)
        {
            if (tabelaGeralItemId != dto.Id) return BadRequest();
            try
            {
                await _tabelaGeralItemService.UpdateAsync(dto);

                var json = JsonSerializer.Serialize(dto);
                var message = JsonSerializer.Deserialize<TabelaGeralItemMessage>(json);
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

        [Route("[action]/{tabelaGeralItemId}")]
        [HttpDelete, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteTabelaGeralItem(Guid tabelaGeralItemId)
        {
            try
            {
                await _tabelaGeralItemService.DeleteAsync(tabelaGeralItemId);
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

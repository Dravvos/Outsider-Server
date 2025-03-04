using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Outsider.AvaliacaoAPI.Services;
using Outsider.AvaliacaoAPI.Utils;
using Outsider.DTO;

namespace Outsider.AvaliacaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoService _service;

        public AvaliacaoController(IAvaliacaoService service)
        {
            _service = service;
        }

        [HttpGet("[action]/{produtoId}")]
        public async Task<IActionResult> GetByProduto(Guid produtoId) 
        {
            try
            {
                var avaliacoes = await _service.GetByProdutoId(produtoId);
                if (avaliacoes == null || avaliacoes.Any() == false) return NotFound();
                return Ok(avaliacoes);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return StatusCode(500, ex.Message);

                return StatusCode(500, ex.InnerException.Message);
            }
        }

        [HttpGet("[action]/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(Guid usuarioId)
        {
            try
            {
                var avaliacoes = await _service.GetByUsuario(usuarioId);
                if (avaliacoes == null || avaliacoes.Any() == false) return NotFound();
                return Ok(avaliacoes);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return StatusCode(500, ex.Message);

                return StatusCode(500, ex.InnerException.Message);
            }
        }

        [HttpPost, Authorize(Roles = Role.Cliente)]
        public async Task<IActionResult> Create([FromBody] AvaliacaoDTO dto)
        {
            if (dto == null) return BadRequest(ModelState);
            try
            {
                await _service.Create(dto);
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

                return StatusCode(500, ex.InnerException.Message);

            }
        }

        [HttpPut("{id}"), Authorize(Roles = Role.Cliente)]
        public async Task<IActionResult> Update(Guid id,[FromBody] AvaliacaoDTO dto)
        {
            if (dto == null) return BadRequest(ModelState);
            if (dto.Id != id) return BadRequest("O Id da requisição é diferente do Id do Body");
            try
            {
                await _service.Update(dto);
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
        [HttpDelete("{id}"), Authorize(Roles = Role.Cliente)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.Delete(id);
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

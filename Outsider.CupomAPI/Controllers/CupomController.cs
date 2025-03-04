using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Outsider.CupomAPI.Service;
using Outsider.CupomAPI.Utils;
using Outsider.DTO;

namespace Outsider.CupomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CupomController : ControllerBase
    {
        private readonly ICupomService _service;

        public CupomController(ICupomService service)
        {
            _service = service;
        }

        [HttpGet, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cupons = await _service.GetAll();
                return Ok(cupons);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{cupomCodigo}")]
        public async Task<IActionResult> GetCuponByCode(string cupomCodigo)
        {
            try
            {
                var cupom = await _service.GetCupomByCodigo(cupomCodigo);
                if (cupom == null)
                    return NotFound();
                return Ok(cupom);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Create([FromBody] CupomDTO dto)
        {
            try
            {
                await _service.Create(dto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                if (ex.InnerException != null)
                    return BadRequest(ex.InnerException.Message);

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}"), Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CupomDTO dto)
        {
            try
            {
                await _service.Update(id, dto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                if (ex.InnerException != null)
                    return BadRequest(ex.InnerException.Message);

                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
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

        [HttpDelete("{id}"), Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeletarCupom(id);
                return Ok();
            }
            catch (KeyNotFoundException)
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

    }
}

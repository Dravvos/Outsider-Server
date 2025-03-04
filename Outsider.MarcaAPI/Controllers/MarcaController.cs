using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Outsider.DTO;
using Outsider.MarcaAPI.Repository;
using Outsider.MarcaAPI.Utils;

namespace Outsider.MarcaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaRepository _repository;

        public MarcaController(IMarcaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var marcas = await _repository.GetAllAsync();
                if (marcas == null || marcas.Any() == false)
                    return NotFound();

                return Ok(marcas);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:guid}"), Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var categoria = await _repository.GetByIdAsync(id);
                if (categoria == null)
                    return NotFound();

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{nome}"), Authorize]
        public async Task<IActionResult> GetByNome(string nome)
        {
            var marcas = await _repository.GetByNomeAsync(nome);
            if (marcas == null || marcas.Any() == false)
                return NotFound();

            return Ok(marcas);
        }

        [Route("[action]/{categoriaId}")]
        [HttpGet, Authorize]
        public async Task<IActionResult> GetByCategoria(Guid categoriaId)
        {
            try
            {
                var marcas = await _repository.GetByCategoriaAsync(categoriaId);
                if (marcas == null || marcas.Any() == false)
                    return NotFound();

                return Ok(marcas);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);

                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Create([FromBody] MarcaDTO dto)
        {
            if (dto == null)
                return BadRequest();

            try
            {
                var categoria = await _repository.AddAsync(dto);
                return Ok(categoria);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}"), Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Update(Guid id, [FromBody] MarcaDTO dto)
        {
            if (dto == null)
                return BadRequest();
            if (dto.Id.HasValue && dto.Id.Value != id)
                return BadRequest("Id do Body é diferente do Id da requisição");
            try
            {
                var aux = await _repository.GetByIdAsync(id);
                dto.DataInclusao = aux.DataInclusao;

                var categoria = await _repository.UpdateAsync(dto);
                return Ok(categoria);
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
                var categoria = await _repository.GetByIdAsync(id);
                if (categoria == null)
                    return NotFound();

                var ret = await _repository.DeleteAsync(id);
                if (!ret) return StatusCode(500, "Não foi possível deletar marca");
                return Ok(ret);
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

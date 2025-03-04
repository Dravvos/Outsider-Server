using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Outsider.CategoriaAPI.Repository;
using Outsider.CategoriaAPI.Utils;
using Outsider.DTO;
using System.Linq;

namespace Outsider.CategoriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaController(ICategoriaRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categorias = await _repository.GetAll();
                if (categorias == null || categorias.Any() == false)
                    return NotFound();

                return Ok(categorias);
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
                var categoria = await _repository.GetById(id);
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
            var categorias = await _repository.GetByNome(nome);
            if (categorias == null || categorias.Any() == false)
                return NotFound();

            return Ok(categorias);
        }

        [HttpPost, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Create([FromBody] CategoriaDTO dto)
        {
            if (dto == null)
                return BadRequest();
            try
            {
                var categoria = await _repository.Create(dto);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoriaDTO dto)
        {
            if (dto == null)
                return BadRequest();
            if (dto.Id.HasValue && dto.Id.Value != id)
                return BadRequest("Id do Body é diferente do Id da requisição");
            try
            {
                var aux = await _repository.GetById(id);
                dto.DataInclusao = aux.DataInclusao;

                var categoria = await _repository.Update(dto);
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

        [HttpDelete("{id}"), Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var categoria = await _repository.GetById(id);
            if (categoria == null)
                return NotFound();
            try
            {
                var ret = await _repository.Delete(id);
                if (!ret) return BadRequest();
                return Ok(ret);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

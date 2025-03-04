using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.CategoriaAPI.Model;
using Outsider.CategoriaAPI.Model.Context;
using Outsider.DTO;

namespace Outsider.CategoriaAPI.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly OutsiderContext con;
        private readonly IMapper _mapper;

        public CategoriaRepository(OutsiderContext con, IMapper mapper)
        {
            this.con = con;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAll()
        {
            var categorias = await con.Categorias.ToListAsync();
            return _mapper.Map<List<CategoriaDTO>>(categorias);
        }

        public async Task<CategoriaDTO> GetById(Guid id)
        {
            var categoria = await con.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<CategoriaDTO>(categoria);
        }

        public async Task<IEnumerable<CategoriaDTO>> GetByNome(string nome)
        {
            var categoria = await con.Categorias.Where(x => x.Nome!.ToLower().Contains(nome.ToLower())).ToListAsync();
            return _mapper.Map<List<CategoriaDTO>>(categoria);
        }

        public async Task<CategoriaDTO> Create(CategoriaDTO dto)
        {
            await ValidaDTO(dto);

            dto.Id = Guid.NewGuid();
            dto.DataInclusao = DateTime.Now;
            var categoria = _mapper.Map<CategoriaModel>(dto);
            await con.Categorias.AddAsync(categoria);
            await con.SaveChangesAsync();
            return _mapper.Map<CategoriaDTO>(categoria);
        }

        public async Task<CategoriaDTO> Update(CategoriaDTO dto)
        {
            await ValidaDTO(dto);

            var categoria = await con.Categorias.FirstAsync(x => x.Id == dto.Id);
            categoria.DataAlteracao = DateTime.Now;
            categoria.Nome = dto.Nome;

            await con.SaveChangesAsync();
            return _mapper.Map<CategoriaDTO>(categoria);
        }

        private async Task ValidaDTO(CategoriaDTO dto)
        {
            var categorias = await GetByNome(dto.Nome);
            if (categorias != null && categorias.Any())
                throw new ArgumentException("Já existe essa categoria");
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var produto = await con.Categorias.FirstOrDefaultAsync(x => x.Id == id);
                if (produto == null)
                    return false;
                con.Categorias.Remove(produto);
                await con.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.DTO;
using Outsider.MarcaAPI.Model;
using Outsider.MarcaAPI.Model.Context;

namespace Outsider.MarcaAPI.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly IMapper _mapper;
        private readonly OutsiderContext _context;

        public MarcaRepository(IMapper mapper, OutsiderContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IList<MarcaDTO>> GetAllAsync()
        {
            var marcas = await _context.Marcas.Include(x => x.Categoria).ToListAsync();
            return _mapper.Map<List<MarcaDTO>>(marcas);
        }

        public async Task<MarcaDTO> GetByIdAsync(Guid id)
        {
            var marca = await _context.Marcas.Include(x => x.Categoria).FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<MarcaDTO>(marca);
        }

        public async Task<MarcaDTO> AddAsync(MarcaDTO dto)
        {
            await ValidaDTO(dto);

            dto.DataInclusao = DateTime.Now;
            dto.Id = Guid.NewGuid();
            var marca = _mapper.Map<MarcaModel>(dto);
            await _context.Marcas.AddAsync(marca);
            await _context.SaveChangesAsync();
            return _mapper.Map<MarcaDTO>(marca);
        }

        public async Task<MarcaDTO> UpdateAsync(MarcaDTO dto)
        {
            await ValidaDTO(dto);

            var marca = await _context.Marcas.FirstAsync(x => x.Id == dto.Id);
            marca.CategoriaId = dto.CategoriaId;
            marca.Nome = dto.Nome;
            marca.DataAlteracao = DateTime.Now;

            await _context.SaveChangesAsync();
            return _mapper.Map<MarcaDTO>(marca);
        }

        private async Task ValidaDTO(MarcaDTO dto)
        {
            var marcas = await _context.Marcas.Where(x => x.CategoriaId == dto.CategoriaId).ToListAsync();
            var marcasNomes = marcas.Select(x => x.Nome).ToList();

            if (marcasNomes.Contains(dto.Nome))
                throw new ArgumentException("Essa marca já foi cadastrada nessa categoria");
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var marca = await _context.Marcas.FirstOrDefaultAsync(x => x.Id == id);
                if (marca == null)
                    return false;
                _context.Marcas.Remove(marca);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<MarcaDTO>> GetByNomeAsync(string nome)
        {
            var marca = await _context.Marcas.Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToListAsync();
            return _mapper.Map<List<MarcaDTO>>(marca);
        }


        public async Task<IList<MarcaDTO>> GetByCategoriaAsync(Guid categoriaId)
        {
            var marca = await _context.Marcas.Where(x => x.CategoriaId == categoriaId).ToListAsync();
            return _mapper.Map<List<MarcaDTO>>(marca);
        }
    }
}

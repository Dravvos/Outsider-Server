using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.DTO;
using Outsider.TabelaGeralAPI.Model;
using Outsider.TabelaGeralAPI.Model.Context;

namespace Outsider.TabelaGeralAPI.Repository
{
    public class TabelaGeralRepository : ITabelaGeralRepository
    {
        private readonly IMapper _mapper;
        private readonly OutsiderContext _context;

        public TabelaGeralRepository(IMapper mapper, OutsiderContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IList<TabelaGeralDTO>> GetAllAsync()
        {
            var tabelasGerais = await _context.TabelasGerais.ToListAsync();
            return _mapper.Map<List<TabelaGeralDTO>>(tabelasGerais);
        }

        public async Task<TabelaGeralDTO> GetByIdAsync(Guid id)
        {
            var tabelaGeral = await _context.TabelasGerais.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<TabelaGeralDTO>(tabelaGeral);
        }

        public async Task<TabelaGeralDTO> GetByNomeAsync(string nome)
        {
            var tabelaGeral = await _context.TabelasGerais.FirstOrDefaultAsync(x => x.Nome == nome);
            return _mapper.Map<TabelaGeralDTO>(tabelaGeral);
        }


        public async Task<TabelaGeralDTO> AddAsync(TabelaGeralDTO dto)
        {
            var tabelaGeral = _mapper.Map<TabelaGeralModel>(dto);
            await _context.TabelasGerais.AddAsync(tabelaGeral);
            await _context.SaveChangesAsync();
            return _mapper.Map<TabelaGeralDTO>(tabelaGeral);
        }

        public async Task<TabelaGeralDTO> UpdateAsync(TabelaGeralDTO dto)
        {
            var tabelaGeral = await _context.TabelasGerais.FirstAsync(x => x.Id == dto.Id);
            tabelaGeral.Descricao = dto.Descricao;
            tabelaGeral.Nome = dto.Nome;
            tabelaGeral.DataAlteracao = DateTime.Now;
            
            await _context.SaveChangesAsync();
            return _mapper.Map<TabelaGeralDTO>(tabelaGeral);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var tabelaGeral = await _context.TabelasGerais.FirstOrDefaultAsync(x => x.Id == id);
                if (tabelaGeral == null)
                    return false;
                _context.TabelasGerais.Remove(tabelaGeral);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

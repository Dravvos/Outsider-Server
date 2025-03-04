using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.DTO;
using Outsider.TabelaGeralAPI.Model;
using Outsider.TabelaGeralAPI.Model.Context;

namespace Outsider.TabelaGeralAPI.Repository
{
    public class TabelaGeralItemRepository : ITabelaGeralItemRepository
    {
        private readonly IMapper _mapper;
        private readonly OutsiderContext _context;

        public TabelaGeralItemRepository(IMapper mapper, OutsiderContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IList<TabelaGeralItemDTO>> GetAllAsync()
        {
            var ItensTabelasGerais = await _context.TabelaGeralItem.Include(x=>x.TabelaGeral).ToListAsync();
            return _mapper.Map<List<TabelaGeralItemDTO>>(ItensTabelasGerais);
        }

        public async Task<IList<TabelaGeralItemDTO>> GetAllItemsAsync(Guid? tabelaGeralId)
        {
            var ItensTabelasGerais = new List<TabelaGeralItemModel>();
            if (tabelaGeralId != null)
                ItensTabelasGerais = await _context.TabelaGeralItem.Where(x => x.TabelaGeralId == tabelaGeralId.Value).Include(x => x.TabelaGeral).ToListAsync();
            else
                ItensTabelasGerais = await _context.TabelaGeralItem.Include(x => x.TabelaGeral).ToListAsync();

            return _mapper.Map<List<TabelaGeralItemDTO>>(ItensTabelasGerais);
        }

        public async Task<TabelaGeralItemDTO> GetByIdAsync(Guid id)
        {
            var tabelaGeralItem = await _context.TabelaGeralItem.Include(x => x.TabelaGeral).FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<TabelaGeralItemDTO>(tabelaGeralItem);
        }

        public async Task<TabelaGeralItemDTO> GetBySiglaAsync(Guid tabelaGeralId, string sigla)
        {
            var tabelaGeralItem = await _context.TabelaGeralItem.Include(x => x.TabelaGeral).FirstOrDefaultAsync(x => x.TabelaGeralId == tabelaGeralId && x.Sigla == sigla);
            return _mapper.Map<TabelaGeralItemDTO>(tabelaGeralItem);
        }

        public async Task<TabelaGeralItemDTO> AddAsync(TabelaGeralItemDTO item)
        {
            var tabelaGeralItem = _mapper.Map<TabelaGeralItemModel>(item);
            await _context.TabelaGeralItem.AddAsync(tabelaGeralItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<TabelaGeralItemDTO>(tabelaGeralItem);
        }

        public async Task<TabelaGeralItemDTO> UpdateAsync(TabelaGeralItemDTO item)
        {
            var model = await _context.TabelaGeralItem.FirstAsync(x => x.Id == item.Id);

            model.Descricao = item.Descricao;
            model.DataAlteracao = DateTime.Now;
            model.Sigla = item.Sigla.ToUpper();

            await _context.SaveChangesAsync();
            return _mapper.Map<TabelaGeralItemDTO>(model);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var tabelaGeralItem = await _context.TabelaGeralItem.FirstOrDefaultAsync(x => x.Id == id);
                if (tabelaGeralItem == null)
                    return false;
                _context.TabelaGeralItem.Remove(tabelaGeralItem);
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

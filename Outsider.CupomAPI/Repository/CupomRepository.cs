using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.CupomAPI.Model;
using Outsider.CupomAPI.Model.Context;
using Outsider.DTO;

namespace Outsider.CupomAPI.Repository
{
    public class CupomRepository : ICupomRepository
    {

        private readonly IMapper _mapper;
        private readonly OutsiderContext _context;

        public CupomRepository(IMapper mapper, OutsiderContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Update(CupomDTO cupom)
        {
            cupom.DataAlteracao = DateTime.Now;
            var model = await _context.Cupons.FirstAsync(x => x.Id == cupom.Id);
            model.CodigoCupom = cupom.CodigoCupom;
            model.PorcentagemDesconto = cupom.PorcentagemDesconto;
            model.DataValidade = cupom.DataValidade;

            await _context.SaveChangesAsync();
        }

        public async Task Create(CupomDTO cupomDTO)
        {
            var cupom = _mapper.Map<CupomModel>(cupomDTO);
            _context.Cupons.Add(cupom);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarCupom(Guid id)
        {
            var cupom = await _context.Cupons.FirstAsync(x => x.Id == id);
            _context.Cupons.Remove(cupom);
            await _context.SaveChangesAsync();
        }

        public async Task<CupomDTO> GetCupomByCodigo(string codigoCupom)
        {
            var cupom = await _context.Cupons.FirstOrDefaultAsync(x => x.CodigoCupom == codigoCupom);
            return _mapper.Map<CupomDTO>(cupom);
        }

        public async Task<IEnumerable<CupomDTO>> GetAll()
        {
            var cupons = await _context.Cupons.ToListAsync();
            return _mapper.Map<List<CupomDTO>>(cupons);
        }

        public async Task<CupomDTO> GetCupomById(Guid id)
        {
            var cupom = await _context.Cupons.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<CupomDTO>(cupom);
        }
    }
}

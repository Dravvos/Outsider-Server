using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.AvaliacaoAPI.Model;
using Outsider.AvaliacaoAPI.Model.Context;
using Outsider.DTO;

namespace Outsider.AvaliacaoAPI.Repository
{
    public class AvaliacaoSqlRepository : IAvaliacaoSqlRepository
    {
        private readonly IMapper _mapper;
        private readonly OutsiderContext _con;

        public AvaliacaoSqlRepository(IMapper mapper, OutsiderContext con)
        {
            _mapper = mapper;
            _con = con;
        }

        public async Task Create(AvaliacaoDTO dto)
        {
            var model = _mapper.Map<AvaliacaoModel>(dto);
            _con.Avaliacoes.Add(model);
            await _con.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var model =await _con.Avaliacoes.FirstAsync(x => x.Id == id);
            _con.Avaliacoes.Remove(model);
            await _con.SaveChangesAsync();
        }

        public async Task<IEnumerable<AvaliacaoDTO>> GetByProdutoId(Guid produtoId)
        {
            var reviews = await _con.Avaliacoes.Where(x => x.ProdutoId == produtoId).ToListAsync();
            return _mapper.Map<List<AvaliacaoDTO>>(reviews);
        }

        public async Task<IEnumerable<AvaliacaoDTO>> GetByUsuario(Guid usuarioId)
        {
            var reviews = await _con.Avaliacoes.Where(x => x.UsuarioId == usuarioId).ToListAsync();
            return _mapper.Map<List<AvaliacaoDTO>>(reviews);
        }

        public async Task Update(AvaliacaoDTO dto)
        {
            var model = await _con.Avaliacoes.FirstAsync(x => x.Id == dto.Id);
            model.Estrelas = dto.Estrelas;
            model.Comentario = dto.Comentario;
            model.DataAvaliacao = dto.DataAvaliacao;
            model.ProdutoId = dto.ProdutoId;
            model.UsuarioId = dto.UsuarioId;
            model.DataAlteracao = dto.DataAlteracao;
            await _con.SaveChangesAsync();
        }
    }
}

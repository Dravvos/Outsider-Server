using AutoMapper;
using MongoDB.Driver;
using Outsider.DTO;
using DataNoSQL;
using Outsider.AvaliacaoAPI.MongoDB;

namespace Outsider.AvaliacaoAPI.Repository
{
    public class AvaliacaoMongoRepository : IAvaliacaoMongoRepository
    {

        private readonly IMapper _mapper;
        private readonly MongoDbContext _con;

        public AvaliacaoMongoRepository(IMapper mapper, MongoDbContext con)
        {
            _mapper = mapper;
            _con = con;
        }

        public async Task Create(AvaliacaoDTO dto)
        {
            var model = _mapper.Map<AVALIACAODTC_NOSQL>(dto);
            await _con.Avaliacoes.InsertOneAsync(model);
        }

        public async Task Delete(Guid id)
        {
            var filtro = FilterHelper<AVALIACAODTC_NOSQL>.BuildEqualityFilter("Id", id);
            await _con.Avaliacoes.DeleteOneAsync(filtro);
        }

        public async Task<AvaliacaoDTO> GetById(Guid id)
        {
            var filtro = FilterHelper<AVALIACAODTC_NOSQL>.BuildEqualityFilter("Id", id);
            var dtc = (await _con.Avaliacoes.FindAsync(filtro)).FirstOrDefault();
            return _mapper.Map<AvaliacaoDTO>(dtc);
        }

        public async Task<IEnumerable<AvaliacaoDTO>> GetByProdutoId(Guid produtoId)
        {
            var dtc = (await _con.Avaliacoes.FindAsync(x => x.ProdutoId == produtoId)).ToList();
            return _mapper.Map<List<AvaliacaoDTO>>(dtc);
        }

        public async Task<IEnumerable<AvaliacaoDTO>> GetByUsuario(Guid usuarioId)
        {
            var filtro = FilterHelper<AVALIACAODTC_NOSQL>.BuildEqualityFilter("UsuarioId", usuarioId);
            var dtc = (await _con.Avaliacoes.FindAsync(filtro)).ToList();
            return _mapper.Map<List<AvaliacaoDTO>>(dtc);
        }

        public async Task Update(AvaliacaoDTO dto)
        {
            var filtro = FilterHelper<AVALIACAODTC_NOSQL>.BuildEqualityFilter("Id", dto.Id.Value);
            var updateDefinition = Builders<AVALIACAODTC_NOSQL>.Update
           .Set(e => e.Estrelas, dto.Estrelas)
           .Set(e => e.Comentario, dto.Comentario)
           .Set(e => e.ProdutoId, dto.ProdutoId)
           .Set(e => e.UsuarioId, dto.UsuarioId)
           .Set(e => e.DataAvaliacao, dto.DataAvaliacao)
           .Set(e => e.DataAlteracao, dto.DataAlteracao);

            await _con.Avaliacoes.UpdateOneAsync(filtro, updateDefinition);
        }
    }
}

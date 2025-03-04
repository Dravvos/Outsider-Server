using AutoMapper;
using DataNoSQL;
using MongoDB.Driver;
using Outside.EnderecoAPI.Model;
using Outsider.DTO;
using Outsider.EnderecoAPI.Model;
using Outsider.EnderecoAPI.Model.Context;

namespace Outsider.EnderecoAPI.Repository
{
    public class EnderecoMongoRepository : IEnderecoMongoRepository
    {
        private readonly IMapper _mapper;
        private readonly MongoDbContext _mongoContext;

        public EnderecoMongoRepository(IMapper mapper, MongoDbContext mongoDbContext)
        {
            _mapper = mapper;
            _mongoContext = mongoDbContext;
        }

        public async Task AddEndereco(EnderecoDTO dto)
        {
            var model = _mapper.Map<ENDERECODTC_NOSQL>(dto);
            await _mongoContext.Enderecos.InsertOneAsync(model);
        }

        public async Task DeleteEndereco(Guid id)
        {
            var filtro = FilterHelper<ENDERECODTC_NOSQL>.BuildEqualityFilter("Id", id);            
            var res = await _mongoContext.Enderecos.DeleteOneAsync(filtro);
        }

        public async Task<IEnumerable<EnderecoDTO>> GetAll(Guid usuarioId)
        {
            var filtro = FilterHelper<ENDERECODTC_NOSQL>.BuildEqualityFilter("UsuarioId", usuarioId);
            var dtc = (await _mongoContext.Enderecos.FindAsync(filtro)).ToList();
            return _mapper.Map<List<EnderecoDTO>>(dtc);
        }

        public async Task<EnderecoDTO> GetById(Guid id)
        {
            var filtro = FilterHelper<ENDERECODTC_NOSQL>.BuildEqualityFilter("Id", id);
            var dtc = (await _mongoContext.Enderecos.FindAsync(filtro)).FirstOrDefault();
            return _mapper.Map<EnderecoDTO>(dtc);
        }

        public async Task UpdateEndereco(EnderecoDTO dto)
        {
            var filtro = FilterHelper<ENDERECODTC_NOSQL>.BuildEqualityFilter("Id", dto.Id);

            var updateDefinition = Builders<ENDERECODTC_NOSQL>.Update
            .Set(e => e.Cidade, dto.Cidade)
            .Set(e => e.Bairro, dto.Bairro)
            .Set(e => e.CEP, dto.CEP)
            .Set(e => e.Estado, dto.Estado)
            .Set(e => e.Logradouro, dto.Logradouro)
            .Set(e => e.Numero, dto.Numero)
            .Set(e => e.Complemento, dto.Complemento)
            .Set(e => e.DataAlteracao, dto.DataAlteracao)
            .Set(e=>e.Recebedor, dto.Recebedor);

            await _mongoContext.Enderecos.UpdateOneAsync(filtro, updateDefinition);
        }
    }
}

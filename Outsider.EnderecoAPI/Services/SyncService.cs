using Outsider.DTO;
using Outsider.EnderecoAPI.Repository;

namespace Outsider.EnderecoAPI.Services
{
    public class SyncService
    {
        private readonly IEnderecoSqlRepository _enderecoSqlRepository;
        private readonly IEnderecoMongoRepository _enderecoMongoRepository;

        public SyncService(IEnderecoSqlRepository enderecoSqlRepository, IEnderecoMongoRepository enderecoMongoRepository)
        {
            _enderecoSqlRepository = enderecoSqlRepository;
            _enderecoMongoRepository = enderecoMongoRepository;
        }

        public async Task AddEndereco(EnderecoDTO dto)
        {
            await _enderecoSqlRepository.AddEndereco(dto);
            await _enderecoMongoRepository.AddEndereco(dto);
        }
        public async Task UpdateEndereco(EnderecoDTO dto)
        {
            await _enderecoSqlRepository.UpdateEndereco(dto);
            await _enderecoMongoRepository.UpdateEndereco(dto);
        }
        public async Task DeleteEndereco(Guid id)
        {
            await _enderecoSqlRepository.DeleteEndereco(id);
            await _enderecoMongoRepository.DeleteEndereco(id);
        }
    }
}

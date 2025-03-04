using Outsider.DTO;
using Outsider.AvaliacaoAPI.Repository;

namespace Outsider.AvaliacaoAPI.Services
{
    public class SyncService
    {
        private readonly IAvaliacaoSqlRepository _avaliacaoSqlRepository;
        private readonly IAvaliacaoMongoRepository _avaliacaoMongoRepository;

        public SyncService(IAvaliacaoSqlRepository avaliacaoSqlRepository, IAvaliacaoMongoRepository avaliacaoMongoRepository)
        {
            _avaliacaoSqlRepository = avaliacaoSqlRepository;
            _avaliacaoMongoRepository = avaliacaoMongoRepository;
        }

        public async Task AddAvaliacao(AvaliacaoDTO dto)
        {
            await _avaliacaoSqlRepository.Create(dto);
            await _avaliacaoMongoRepository.Create(dto);
        }
        public async Task UpdateAvaliacao(AvaliacaoDTO dto)
        {
            await _avaliacaoSqlRepository.Update(dto);
            await _avaliacaoMongoRepository.Update(dto);
        }
        public async Task DeleteAvaliacao(Guid id)
        {
            await _avaliacaoSqlRepository.Delete(id);
            await _avaliacaoMongoRepository.Delete(id);
        }
    }
}

using Outsider.AvaliacaoAPI.Repository;
using Outsider.DTO;

namespace Outsider.AvaliacaoAPI.Services
{
    public class AvaliacaoService:IAvaliacaoService
    {
        private SyncService _syncService;
        private readonly IAvaliacaoMongoRepository _avaliacaoMongoRepository;

        public AvaliacaoService(SyncService syncService, IAvaliacaoMongoRepository enderecoMongoRepository)
        {
            _syncService = syncService;
            _avaliacaoMongoRepository = enderecoMongoRepository;
        }

        private void ValidaAvaliacao(AvaliacaoDTO dto)
        {
            if (dto.Estrelas <= 0)
                throw new ArgumentOutOfRangeException("O mínimo de estrelas para um produto é 1(uma)");

            if (dto.ProdutoId == Guid.Empty)
                throw new ArgumentNullException("Selecione o produto que deseja avaliar");

            if (dto.UsuarioId == Guid.Empty)
                throw new ArgumentNullException("A avaliação precisa ser feita por um usuário");

            if (dto.DataAvaliacao > DateTime.Now)
                throw new ArgumentOutOfRangeException("Não é possível avaliar algo amanhã");
        }

        public async Task Create(AvaliacaoDTO dto)
        {
            ValidaAvaliacao(dto);

            dto.Id = Guid.NewGuid();
            dto.DataInclusao = DateTime.Now;

            await _syncService.AddAvaliacao(dto);
        }

        public async Task Delete(Guid id)
        {
            if ((await _avaliacaoMongoRepository.GetById(id)) == null)
                throw new KeyNotFoundException();

            await _syncService.DeleteAvaliacao(id);
        }

        public async Task<IEnumerable<AvaliacaoDTO>> GetByProdutoId(Guid produtoId)
        {
            return await _avaliacaoMongoRepository.GetByProdutoId(produtoId);
        }

        public async Task<IEnumerable<AvaliacaoDTO>> GetByUsuario(Guid usuarioId)
        {
            return await _avaliacaoMongoRepository.GetByUsuario(usuarioId);
        }

        public async Task Update(AvaliacaoDTO dto)
        {
            ValidaAvaliacao(dto);

            dto.DataAlteracao = DateTime.Now;

            await _syncService.UpdateAvaliacao(dto);
        }
    }
}

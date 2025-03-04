using Outsider.DTO;
using Outsider.EnderecoAPI.Repository;

namespace Outsider.EnderecoAPI.Services
{
    public class EnderecoService : IEnderecoService
    {
        private SyncService _syncService;
        private readonly IEnderecoMongoRepository _enderecoMongoRepository;

        public EnderecoService(SyncService syncService, IEnderecoMongoRepository enderecoMongoRepository)
        {
            _syncService = syncService;
            _enderecoMongoRepository = enderecoMongoRepository;
        }

        public async Task AddEndereco(EnderecoDTO dto)
        {
            ValidaEndereco(dto);

            dto.Id = Guid.NewGuid();
            dto.DataInclusao = DateTime.Now;

            await _syncService.AddEndereco(dto);
        }

        private void ValidaEndereco(EnderecoDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Recebedor))
                throw new ArgumentNullException("Digite quem vai receber o pedido");
            if (string.IsNullOrEmpty(dto.CEP))
                throw new ArgumentNullException("Digite seu CEP");
            if (string.IsNullOrEmpty(dto.Logradouro))
                throw new ArgumentNullException("Digite seu endereço");
            if (string.IsNullOrEmpty(dto.Bairro))
                throw new ArgumentNullException("Digite seu bairro");
            if (string.IsNullOrEmpty(dto.Cidade))
                throw new ArgumentNullException("Digite sua cidade");
            if (dto.Numero <= 0)
                throw new ArgumentOutOfRangeException("O número do endereço deve ser maior que zero");
            if (string.IsNullOrEmpty(dto.Estado))
                throw new ArgumentNullException("Digite seu estado");
        }

        public async Task DeleteEndereco(Guid id)
        {
            if ((await _enderecoMongoRepository.GetById(id)) == null)
                throw new KeyNotFoundException();

            await _syncService.DeleteEndereco(id);
        }

        public async Task<IEnumerable<EnderecoDTO>> GetAll(Guid usuarioId)
        {
            return await _enderecoMongoRepository.GetAll(usuarioId);
        }

        public async Task<EnderecoDTO> GetById(Guid id)
        {
            return await _enderecoMongoRepository.GetById(id);
        }

        public async Task UpdateEndereco(EnderecoDTO dto)
        {
            if( (await _enderecoMongoRepository.GetById(dto.Id.Value))==null)
                throw new KeyNotFoundException();

            ValidaEndereco(dto);
            dto.DataAlteracao = DateTime.Now;
            await _syncService.UpdateEndereco(dto);
        }
    }
}

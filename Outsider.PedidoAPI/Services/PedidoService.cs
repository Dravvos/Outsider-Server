using Outsider.DTO;
using Outsider.PedidoAPI.Repository;

namespace Outsider.PedidoAPI.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PedidoItemDTO>> GetByUser(Guid usuardioId)
        {
            return await _repository.GetByUser(usuardioId);
        }
    }
}

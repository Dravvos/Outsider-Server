using Outsider.DTO;

namespace Outsider.PedidoAPI.Services
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoItemDTO>> GetByUser(Guid usuardioId);

    }
}

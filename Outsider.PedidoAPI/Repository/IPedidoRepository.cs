using Outsider.DTO;
using Outsider.PedidoAPI.Model;

namespace Outsider.PedidoAPI.Repository
{
    public interface IPedidoRepository
    {
        Task<bool> AdicionarPedido(PedidoModel dto);
        Task AtualizarStatusPedido(Guid pedidoId, bool pago);
        Task<IEnumerable<PedidoItemDTO>> GetByUser(Guid usuarioId);
    }
}

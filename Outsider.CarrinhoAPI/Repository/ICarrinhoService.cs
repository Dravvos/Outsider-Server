using Outsider.DTO;

namespace Outsider.CarrinhoAPI.Repository
{
    public interface ICarrinhoRepository
    {
        Task<IEnumerable<ItemCarrinhoDTO>> GetCarrinhoPorUsuario(Guid usuarioId);
        Task AdicionarItemAoCarrinho(Guid usuarioId, Guid produtoId, int quantidade);
        Task<bool> RemoverItemDoCarrinho(Guid itemCarrinhoId);
        Task<bool> InserirCupom(Guid usuarioId, string cupom);
        Task<bool> RemoverCupom(Guid usuarioId);
        Task<bool> LimparCarrinho(Guid usuarioId);
    }
}

using Outsider.DTO;

namespace Outsider.CarrinhoAPI.Service
{
    public interface ICarrinhoService
    {
        Task<IEnumerable<ItemCarrinhoDTO>> GetCarrinhoPorUsuario(Guid usuarioId);
        Task AdicionarItemAoCarrinho(Guid usuarioId, Guid produtoId, int quantidade);
        Task RemoverItemDoCarrinho(Guid itemCarrinhoId);
        Task InserirCupom(Guid usuarioId, string cupom, string token);
        Task RemoverCupom(Guid usuarioId);
        Task LimparCarrinho(Guid usuarioId);
    }
}

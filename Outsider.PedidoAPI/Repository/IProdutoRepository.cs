using Outsider.PedidoAPI.Model;
using Outsider.DTO;

namespace Outsider.PedidoAPI.Repository
{
    public interface IProdutoRepository
    {
        Task<ProdutoModel> GetById(Guid id);
        Task Create(ProdutoModel dto);
        Task Update(ProdutoModel dto);
        Task Delete(Guid id);
    }
}

using Outsider.CarrinhoAPI.Model;
using Outsider.DTO;

namespace Outsider.CarrinhoAPI.Repository
{
    public interface IProdutoRepository
    {
        Task Create(ProdutoModel dto);
        Task Update(ProdutoModel dto);
        Task Delete(Guid id);
    }
}

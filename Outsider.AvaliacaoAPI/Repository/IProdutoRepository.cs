using Outsider.AvaliacaoAPI.Model;
using Outsider.DTO;

namespace Outsider.AvaliacaoAPI.Repository
{
    public interface IProdutoRepository
    {
        Task Create(ProdutoModel dto);
        Task Update(ProdutoModel dto);
        Task Delete(Guid id);
    }
}



using Outsider.DTO;
using Outsider.ProdutoAPI.Model;

namespace Outsider.ProdutoAPI.Repository
{
    public interface ITabelaGeralItemRepository
    {
        Task AddAsync(TabelaGeralItemModel model);
        Task UpdateAsync(TabelaGeralItemModel model);
        Task DeleteAsync(Guid id);
    }
}

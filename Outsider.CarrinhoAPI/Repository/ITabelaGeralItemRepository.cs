using Outsider.DTO;
using Outsider.CarrinhoAPI.Model;

namespace Outsider.CarrinhoAPI.Repository
{
    public interface ITabelaGeralItemRepository
    {
        Task AddAsync(TabelaGeralItemModel model);
        Task UpdateAsync(TabelaGeralItemModel model);
        Task DeleteAsync(Guid id);
    }
}

using Outsider.PedidoAPI.Model;

namespace Outsider.PedidoAPI.Repository
{
    public interface ITabelaGeralItemRepository
    {
        Task AddAsync(TabelaGeralItemModel model);
        Task UpdateAsync(TabelaGeralItemModel model);
        Task DeleteAsync(Guid id);
    }
}

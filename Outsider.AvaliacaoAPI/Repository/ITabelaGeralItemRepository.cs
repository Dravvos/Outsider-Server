using Outsider.DTO;
using Outsider.AvaliacaoAPI.Model;

namespace Outsider.AvaliacaoAPI.Repository
{
    public interface ITabelaGeralItemRepository
    {
        Task AddAsync(TabelaGeralItemModel model);
        Task UpdateAsync(TabelaGeralItemModel model);
        Task DeleteAsync(Guid id);
    }
}

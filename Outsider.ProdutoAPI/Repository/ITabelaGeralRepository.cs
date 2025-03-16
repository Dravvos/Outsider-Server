using Outsider.DTO;
using Outsider.ProdutoAPI.Model;

namespace Outsider.ProdutoAPI.Repository
{
    public interface ITabelaGeralRepository
    {
        Task AddAsync(TabelaGeralModel model);
        Task UpdateAsync(TabelaGeralModel model);
        Task DeleteAsync(Guid id);
    }
}

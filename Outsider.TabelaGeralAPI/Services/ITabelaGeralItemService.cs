using Outsider.DTO;

namespace Outsider.TabelaGeralAPI.Services
{
    public interface ITabelaGeralItemService
    {
        Task<TabelaGeralItemDTO> GetByIdAsync(Guid id);
        Task<TabelaGeralItemDTO> GetBySiglaAsync(Guid tabelaGeralId, string sigla);
        Task<IEnumerable<TabelaGeralItemDTO>> GetAllItemsAsync(Guid? tabelaGeralId);
        Task AddAsync(TabelaGeralItemDTO model);
        Task UpdateAsync(TabelaGeralItemDTO model);
        Task DeleteAsync(Guid id);
    }
}

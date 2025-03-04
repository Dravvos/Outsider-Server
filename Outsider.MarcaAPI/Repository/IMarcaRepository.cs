using Outsider.DTO;

namespace Outsider.MarcaAPI.Repository
{
    public interface IMarcaRepository
    {
        Task<MarcaDTO> GetByIdAsync(Guid id);
        Task<IList<MarcaDTO>> GetAllAsync();
        Task<IList<MarcaDTO>> GetByCategoriaAsync(Guid categoriaId);
        Task<IList<MarcaDTO>> GetByNomeAsync(string nome);
        Task<MarcaDTO> AddAsync(MarcaDTO item);
        Task<MarcaDTO> UpdateAsync(MarcaDTO item);
        Task<bool> DeleteAsync(Guid id);
    }
}

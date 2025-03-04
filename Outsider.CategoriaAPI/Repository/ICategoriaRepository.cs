using Outsider.DTO;

namespace Outsider.CategoriaAPI.Repository
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<CategoriaDTO>> GetAll();
        Task<CategoriaDTO> GetById(Guid id);
        Task<IEnumerable<CategoriaDTO>> GetByNome(string nome);
        Task<CategoriaDTO> Create(CategoriaDTO dto);
        Task<CategoriaDTO> Update (CategoriaDTO dto);
        Task<bool> Delete(Guid id);
    }
}

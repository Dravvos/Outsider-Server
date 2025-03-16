using Outsider.DTO;

namespace Outsider.ProdutoAPI.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDTO>> GetAllAdmin();
        Task<IEnumerable<ProdutoDTO>> GetAllClient();
        Task<ProdutoDTO> GetById(Guid id);
        Task<IEnumerable<ProdutoDTO>> GetAllEmEstoque(Guid categoriaId, Guid corId, string nome);
        Task Create(ProdutoDTO dto);
        Task Update(ProdutoDTO dto);
        Task Delete(Guid id);
    }
}

using Outsider.DTO;

namespace Outsider.ProdutoAPI.Repository
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<ProdutoDTO>> GetAllAdmin();
        Task<IEnumerable<ProdutoDTO>> GetAllClient();
        Task<ProdutoDTO> GetById(Guid id);
        Task<IEnumerable<ProdutoDTO>> GetAllEmEstoque(Guid categoriaId, Guid corId, string nome);
        Task<ProdutoDTO> Create(ProdutoDTO dto);
        Task<ProdutoDTO> Update(ProdutoDTO dto);
        Task<bool> Delete(Guid id);
    }
}

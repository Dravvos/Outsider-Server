using Outsider.DTO;

namespace Outsider.AvaliacaoAPI.Repository
{
    public interface IAvaliacaoSqlRepository
    {
        Task<IEnumerable<AvaliacaoDTO>> GetByProdutoId(Guid produtoId);
        Task<IEnumerable<AvaliacaoDTO>> GetByUsuario(Guid usuarioId);
        Task Create(AvaliacaoDTO dto);
        Task Update(AvaliacaoDTO dto);
        Task Delete(Guid id);
    }
}

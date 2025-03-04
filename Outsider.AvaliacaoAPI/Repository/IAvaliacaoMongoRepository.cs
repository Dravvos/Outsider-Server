using Outsider.DTO;

namespace Outsider.AvaliacaoAPI.Repository
{
    public interface IAvaliacaoMongoRepository
    {
        Task<AvaliacaoDTO> GetById(Guid id);
        Task<IEnumerable<AvaliacaoDTO>> GetByProdutoId(Guid produtoId);
        Task<IEnumerable<AvaliacaoDTO>> GetByUsuario(Guid usuarioId);
        Task Create(AvaliacaoDTO dto);
        Task Update(AvaliacaoDTO dto);
        Task Delete(Guid id);
    }
}

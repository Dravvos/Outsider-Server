using Outsider.DTO;

namespace Outsider.EnderecoAPI.Repository
{
    public interface IEnderecoMongoRepository
    {
        Task<EnderecoDTO> GetById(Guid id);
        Task<IEnumerable<EnderecoDTO>> GetAll(Guid usuarioId);
        Task AddEndereco(EnderecoDTO dto);
        Task UpdateEndereco(EnderecoDTO dto);
        Task DeleteEndereco(Guid id);
    }
}

using Outside.PedidoAPI.Model;

namespace Outsider.PedidoAPI.Repository
{
    public interface IEnderecoRepository
    {
        Task AddEndereco(EnderecoModel dto);
        Task UpdateEndereco(EnderecoModel dto);
        Task DeleteEndereco(Guid id);

    }
}

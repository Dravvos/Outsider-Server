using Outsider.CarrinhoAPI.Repository;
using Outsider.DTO;

namespace Outsider.CarrinhoAPI.Service
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly ICupomService _cupomRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository, ICupomService cupomRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _cupomRepository = cupomRepository;
        }

        public async Task AdicionarItemAoCarrinho(Guid usuarioId, Guid produtoId, int quantidade)
        {

            await _carrinhoRepository.AdicionarItemAoCarrinho(usuarioId, produtoId, quantidade);
        }

        public async Task<IEnumerable<ItemCarrinhoDTO>> GetCarrinhoPorUsuario(Guid usuarioId)
        {
            return await _carrinhoRepository.GetCarrinhoPorUsuario(usuarioId);
        }

        public async Task InserirCupom(Guid usuarioId, string cupom, string token)
        {
            if ((await _cupomRepository.GetCupom(cupom, token)) == null)
                throw new KeyNotFoundException();

            await _carrinhoRepository.InserirCupom(usuarioId, cupom);
        }

        public async Task LimparCarrinho(Guid usuarioId)
        {
            await _carrinhoRepository.LimparCarrinho(usuarioId);
        }

        public async Task RemoverCupom(Guid usuarioId)
        {
            await _carrinhoRepository.RemoverCupom(usuarioId);
        }

        public async Task RemoverItemDoCarrinho(Guid itemCarrinhoId)
        {
            await _carrinhoRepository.RemoverItemDoCarrinho(itemCarrinhoId);
        }
    }
}

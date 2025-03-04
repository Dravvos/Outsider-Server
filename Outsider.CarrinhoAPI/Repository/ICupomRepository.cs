using Outsider.DTO;

namespace Outsider.CarrinhoAPI.Repository
{
    public interface ICupomRepository
    {
        Task<CupomDTO> GetCupom(string codigoCupom, string token);
    }
}

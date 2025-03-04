using Outsider.DTO;

namespace Outsider.CarrinhoAPI.Service
{
    public interface ICupomService
    {
        Task<CupomDTO> GetCupom(string codigoCupom, string token);
    }
}

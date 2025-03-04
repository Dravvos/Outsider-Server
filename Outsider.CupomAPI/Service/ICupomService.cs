using Outsider.DTO;

namespace Outsider.CupomAPI.Service
{
    public interface ICupomService
    {
        Task<IEnumerable<CupomDTO>> GetAll();
        Task<CupomDTO> GetCupomByCodigo(string codigoCupom);
        Task Create(CupomDTO cupomDTO);
        Task Update(Guid id, CupomDTO cupom);
        Task DeletarCupom(Guid id);
    }
}

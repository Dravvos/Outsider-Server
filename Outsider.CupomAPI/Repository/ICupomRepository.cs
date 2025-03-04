using Outsider.DTO;

namespace Outsider.CupomAPI.Repository
{
    public interface ICupomRepository
    {
        Task<IEnumerable<CupomDTO>> GetAll();
        Task <CupomDTO> GetCupomByCodigo(string codigoCupom);
        Task<CupomDTO> GetCupomById(Guid id);
        Task Create(CupomDTO cupomDTO);
        Task Update(CupomDTO cupom);
        Task DeletarCupom(Guid id);
    }
}

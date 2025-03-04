using Outsider.CupomAPI.Repository;
using Outsider.DTO;

namespace Outsider.CupomAPI.Service
{
    public class CupomService : ICupomService
    {
        private readonly ICupomRepository _repository;

        public CupomService(ICupomRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(CupomDTO cupomDTO)
        {
            cupomDTO.DataInclusao = DateTime.Now;
            cupomDTO.Id = Guid.NewGuid();
            ValidaCupom(cupomDTO);

            await _repository.Create(cupomDTO);
        }

        private void ValidaCupom(CupomDTO cupomDTO)
        {
            if (string.IsNullOrEmpty(cupomDTO.CodigoCupom))
                throw new ArgumentNullException("Código do cupom não pode estar vazio");
            if (cupomDTO.DataValidade <= DateTime.Now.Date)
                throw new ArgumentException("O cupom deve vencer depois de hoje");
            if (cupomDTO.PorcentagemDesconto >= 100)
                throw new ArgumentException("O máximo de desconto é 99%");
        }

        public async Task DeletarCupom(Guid id)
        {
            if ((await _repository.GetCupomById(id)) == null)
                throw new KeyNotFoundException("Cupom não existe");
            await _repository.DeletarCupom(id);
        }

        public async Task<CupomDTO> GetCupomByCodigo(string codigoCupom)
        {
            return await _repository.GetCupomByCodigo(codigoCupom);
        }

        public async Task Update(Guid id, CupomDTO cupom)
        {
            if (id != cupom.Id)
                throw new ArgumentException("O Id da requisição é diferente do Id do body");
            if ((await _repository.GetCupomById(cupom.Id.Value)) == null)
                throw new KeyNotFoundException();

            ValidaCupom(cupom);
            cupom.DataAlteracao = DateTime.Now;
            await _repository.Update(cupom);
        }

        public async Task<IEnumerable<CupomDTO>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}

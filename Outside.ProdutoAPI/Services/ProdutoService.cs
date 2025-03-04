using Outsider.DTO;
using Outsider.ProdutoAPI.Repository;

namespace Outsider.ProdutoAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(ProdutoDTO dto)
        {
            ValidaProduto(dto);
            dto.Id = Guid.NewGuid();
            dto.DataInclusao = DateTime.Now;
            await _repository.Create(dto);
        }

        private void ValidaProduto(ProdutoDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nome))
                throw new ArgumentNullException("Digite o nome do produto");

            if (string.IsNullOrEmpty(dto.Descricao))
                throw new ArgumentNullException("Digite a descrição do produto");

            if (string.IsNullOrEmpty(dto.ImagemBase64))
                throw new ArgumentNullException("Escolha a foto/imagem do produto");

            if (dto.IdTGTamanho == Guid.Empty)
                throw new ArgumentNullException("Escolha um tamanho de roupa");

            if (dto.IdTGCategoria == Guid.Empty)
                throw new ArgumentNullException("Escolha uma categoria");

            if (dto.IdTGCor == Guid.Empty)
                throw new ArgumentNullException("Escolha uma cor");

            if (string.IsNullOrEmpty(dto.SKU))
                throw new ArgumentNullException("SKU não foi gerado");

            if (dto.Estoque <= 0)
                throw new ArgumentOutOfRangeException("O produto precisa ter pelo menos 1 item em estoque");

            if (dto.Preco <= 0)
                throw new ArgumentOutOfRangeException("O preço do produto precisa ser maior que zero");

            if (dto.Peso < 0)
                throw new ArgumentOutOfRangeException("O peso do produto precisa ser maior que zero. Necessário para calcular o frete");
        }

        public async Task Delete(Guid id)
        {
            if ((await _repository.GetById(id)) == null)
                throw new KeyNotFoundException();

            await _repository.Delete(id);
        }

        public async Task<IEnumerable<ProdutoDTO>> GetAllAdmin()
        {
            return await _repository.GetAllAdmin();
        }

        public async Task<IEnumerable<ProdutoDTO>> GetAllClient()
        {
            return await _repository.GetAllClient();
        }

        public async Task<IEnumerable<ProdutoDTO>> GetAllEmEstoque(Guid categoriaId, Guid corId, string nome)
        {
            return await _repository.GetAllEmEstoque(categoriaId, corId,nome);
        }

        public async Task<ProdutoDTO> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(ProdutoDTO dto)
        {
            ValidaProduto(dto);
            dto.DataAlteracao = DateTime.Now;
            await _repository.Update(dto);
        }
    }
}

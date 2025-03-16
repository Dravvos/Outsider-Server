using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.ProdutoAPI.Model;
using Outsider.ProdutoAPI.Model.Context;
using Outsider.DTO;
using System.Text;

namespace Outsider.ProdutoAPI.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly OutsiderContext _context;
        private readonly IMapper _mapper;

        public ProdutoRepository(OutsiderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetAllAdmin()
        {
            var produtos = await _context.Produtos.Include(x => x.Categoria.TabelaGeral)
                .Include(x => x.Cor.TabelaGeral).Include(x => x.Tamanho.TabelaGeral).ToListAsync();

            var products = _mapper.Map<List<ProdutoDTO>>(produtos);
            for (int i = 0; i < produtos.Count; i++)
            {
                products[i].ImagemBase64 = Encoding.UTF8.GetString(produtos[i].Imagem);
            }
            return products;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetAllClient()
        {
            var produtos = await _context.Produtos.Include(x => x.Categoria).Include(x => x.Cor).Include(x => x.Tamanho)
                .GroupBy(z => new { z.Nome, z.IdTGCategoria, z.Descricao}).Select(z => z.First()).ToListAsync();
            var products = _mapper.Map<List<ProdutoDTO>>(produtos);
            for (int i = 0; i < produtos.Count; i++)
            {
                products[i].ImagemBase64 = Encoding.UTF8.GetString(produtos[i].Imagem);
            }
            return products;
        }


        public async Task<ProdutoDTO> GetById(Guid id)
        {
            var produto = await _context.Produtos.Include(x => x.Categoria).Include(x => x.Cor).Include(x => x.Tamanho).FirstOrDefaultAsync(x => x.Id == id);
            var product = _mapper.Map<ProdutoDTO>(produto);
            product.ImagemBase64 = Encoding.UTF8.GetString(produto.Imagem);
            return product;
        }

        public async Task<ProdutoDTO> Create(ProdutoDTO dto)
        {
            ProdutoModel produto = _mapper.Map<ProdutoModel>(dto);
            await _context.Produtos.AddAsync(produto);
            var entries = _context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task<ProdutoDTO> Update(ProdutoDTO dto)
        {
            var produto = await _context.Produtos.FirstAsync(x => x.Id == dto.Id);
            produto.Descricao = dto.Descricao;
            produto.Nome = dto.Nome;
            produto.SKU = dto.SKU;
            produto.Preco = dto.Preco;
            produto.IdTGCategoria = dto.IdTGCategoria;
            produto.Estoque = dto.Estoque;
            produto.Imagem = dto.Imagem;
            produto.Peso = dto.Peso;
            produto.IdTGCor = dto.IdTGCor;
            produto.IdTGTamanho = dto.IdTGTamanho;
            produto.DataAlteracao = dto.DataAlteracao;
            await _context.SaveChangesAsync();
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
                if (produto == null)
                    return false;
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProdutoDTO>> GetAllEmEstoque(Guid categoriaId, Guid corId, string nome)
        {
            var produtos = await _context.Produtos.Where(x => x.IdTGCategoria == categoriaId && x.IdTGCor == corId && x.Estoque > 0 && x.Nome.ToUpper()==nome.ToUpper())
                .Include(x => x.Categoria).Include(x => x.Cor).Include(x => x.Tamanho).ToListAsync();

            var products = _mapper.Map<List<ProdutoDTO>>(produtos);
            for (int i = 0; i < produtos.Count; i++)
            {
                products[i].ImagemBase64 = Encoding.UTF8.GetString(produtos[i].Imagem);
            }
            return products;
        }
    }
}

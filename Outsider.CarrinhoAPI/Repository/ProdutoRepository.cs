using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.CarrinhoAPI.Model;
using Outsider.CarrinhoAPI.Model.Context;
using Outsider.DTO;
using System.Text;

namespace Outsider.CarrinhoAPI.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DbContextOptions<OutsiderContext> _con;

        public ProdutoRepository(DbContextOptions<OutsiderContext> con)
        {
            _con = con;
        }

        public async Task Create(ProdutoModel model)
        {
            await using var db = new OutsiderContext(_con);
            model.Tamanho = null;
            model.Cor = null; 
            model.Categoria = null;
            await db.Produto.AddAsync(model);
            await db.SaveChangesAsync();
        }

        public async Task Update(ProdutoModel model)
        {
            await using var db = new OutsiderContext(_con);
            var Produto = await db.Produto.FirstAsync(x => x.Id == model.Id);
            Produto.Descricao = model.Descricao;
            Produto.Nome = model.Nome;
            Produto.SKU = model.SKU;
            Produto.Preco = model.Preco;
            Produto.IdTGCategoria = model.IdTGCategoria;
            Produto.Estoque = model.Estoque;
            Produto.Imagem = model.Imagem;
            Produto.Peso = model.Peso;
            Produto.IdTGCor = model.IdTGCor;
            Produto.IdTGTamanho = model.IdTGTamanho;
            Produto.DataAlteracao = model.DataAlteracao;
            await db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await using var _context = new OutsiderContext(_con);
            var Produto = await _context.Produto.FirstOrDefaultAsync(x => x.Id == id);
            _context.Produto.Remove(Produto);
            await _context.SaveChangesAsync();

        }
    }
}

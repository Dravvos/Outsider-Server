using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.DTO;
using Outsider.ProdutoAPI.Model;
using Outsider.ProdutoAPI.Model.Context;

namespace Outsider.ProdutoAPI.Repository
{
    public class TabelaGeralItemRepository : ITabelaGeralItemRepository
    {
        private readonly DbContextOptions<OutsiderContext> _con;

        public TabelaGeralItemRepository(DbContextOptions<OutsiderContext> con)
        {
            _con = con;
        }

        public async Task AddAsync(TabelaGeralItemModel item)
        {
            try
            {
                await using var _context = new OutsiderContext(_con);
                await _context.TabelaGeralItem.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                await using var _context = new OutsiderContext(_con);
                item.TabelaGeral = null;
                await _context.TabelaGeralItem.AddAsync(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(TabelaGeralItemModel item)
        {
            await using var _context = new OutsiderContext(_con);
            var model = await _context.TabelaGeralItem.FirstAsync(x => x.Id == item.Id);
            model.TabelaGeral = null;
            model.Descricao = item.Descricao;
            model.DataAlteracao = DateTime.Now;
            model.Sigla = item.Sigla.ToUpper();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await using var _context = new OutsiderContext(_con);
            var tabelaGeralItem = await _context.TabelaGeralItem.FirstOrDefaultAsync(x => x.Id == id);

            _context.TabelaGeralItem.Remove(tabelaGeralItem);
            await _context.SaveChangesAsync();


        }

    }
}

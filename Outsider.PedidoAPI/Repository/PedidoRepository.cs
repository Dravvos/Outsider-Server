using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.DTO;
using Outsider.PedidoAPI.Model;
using Outsider.PedidoAPI.Model.Context;

namespace Outsider.PedidoAPI.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DbContextOptions<OutsiderContext> _con;
        private readonly IMapper _mapper;

        public PedidoRepository(DbContextOptions<OutsiderContext> con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }

        public async Task<bool> AdicionarPedido(PedidoModel model)
        {
            if (model == null)
                return false;

            await using var db = new OutsiderContext(_con);
            db.Pedidos.Add(model);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task AtualizarStatusPedido(Guid pedidoId, bool pago)
        {
            await using var db = new OutsiderContext(_con);
            var pedido = await db.Pedidos.FirstOrDefaultAsync(x => x.Id == pedidoId);
            if (pedido != null)
            {
                pedido.Pago = pago;
                await db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PedidoItemDTO>> GetByUser(Guid usuarioId)
        {
            await using var db = new OutsiderContext(_con);
            var pedidos = await db.PedidoItens.Where(x => x.Pedido.UsuarioId == usuarioId).Include(x=>x.Pedido).Include(x=>x.Produto).ToListAsync();
            return _mapper.Map<List<PedidoItemDTO>>(pedidos);
        }
    }
}

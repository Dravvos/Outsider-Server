using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.CarrinhoAPI.Model;
using Outsider.CarrinhoAPI.Model.Context;
using Outsider.DTO;
using StackExchange.Redis;
using NRedisStack.RedisStackCommands;
using System.Text;
using System.Text.Json;

namespace Outsider.CarrinhoAPI.Repository
{
    public class CarrinhoRepository : ICarrinhoRepository
    {

        private readonly OutsiderContext con;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _redisCon;

        public CarrinhoRepository(OutsiderContext con, IMapper mapper, IConnectionMultiplexer redisCon)
        {
            this.con = con;
            _mapper = mapper;
            _redisCon = redisCon;
        }

        public async Task AdicionarItemAoCarrinho(Guid usuarioId, Guid produtoId, int quantidade)
        {
            var carrinho = await con.Carrinho.AsNoTracking().FirstOrDefaultAsync(x => x.UsuarioId == usuarioId);

            var itensCarrinho = new List<ItemCarrinhoModel>();
            if (carrinho == null)
            {
                con.Carrinho.Add(new CarrinhoModel
                {
                    Id = Guid.NewGuid(),
                    DataInclusao = DateTime.Now,
                    UsuarioId = usuarioId,
                });
                await con.SaveChangesAsync();
            }
            else
                itensCarrinho = await con.ItensCarrinho.Where(x => x.CarrinhoId == carrinho.Id).ToListAsync();

            if (itensCarrinho == null || itensCarrinho.Any() == false)
            {
                if (carrinho == null)
                    carrinho = await con.Carrinho.FirstAsync(x => x.UsuarioId == usuarioId);

                con.ItensCarrinho.Add(new ItemCarrinhoModel
                {
                    CarrinhoId = carrinho.Id,
                    DataInclusao = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ProdutoId = produtoId,
                    Quantidade = quantidade,
                });
            }
            else
            {
                var produtosNoCarrinho = await con.ItensCarrinho.Where(x => x.CarrinhoId == carrinho.Id).Select(x => x.ProdutoId).ToListAsync();
                if (produtosNoCarrinho.Contains(produtoId))
                {
                    var itemParaModificar = await con.ItensCarrinho.FirstAsync(x => x.ProdutoId == produtoId && x.CarrinhoId == carrinho.Id);
                    itemParaModificar.Quantidade = quantidade;
                    itemParaModificar.DataAlteracao = DateTime.Now;
                }
                else
                {
                    con.ItensCarrinho.Add(new ItemCarrinhoModel
                    {
                        CarrinhoId = carrinho.Id,
                        DataInclusao = DateTime.Now,
                        Id = Guid.NewGuid(),
                        ProdutoId = produtoId,
                        Quantidade = quantidade,
                    });
                }

            }
            await con.SaveChangesAsync();

            await SalvarCarrinhoRedis(usuarioId);
        }

        private async Task SalvarCarrinhoRedis(Guid usuarioId)
        {
            var db = _redisCon.GetDatabase();
            var json = db.JSON();

            var cart = await con.Carrinho.FirstAsync(x => x.UsuarioId == usuarioId);
            var cartItems = await con.ItensCarrinho.Where(x => x.CarrinhoId == cart.Id).Include(x => x.Carrinho).Include(x => x.Produto)
                .Include(x => x.Produto.Cor).Include(x => x.Produto.Tamanho).Include(x => x.Produto.Categoria).ToListAsync();

            var carrinhoRedis = new CarrinhoRedisDTO();
            carrinhoRedis.Id = cart.Id;
            carrinhoRedis.UsuarioId = cart.UsuarioId;
            carrinhoRedis.CodigoCupom = cart.CodigoCupom;
            carrinhoRedis.DataInclusao = cart.DataInclusao;
            carrinhoRedis.DataAlteracao = cart.DataAlteracao;
            carrinhoRedis.ItensCarrinho = new List<CarrinhoItemRedisDTO>();
            foreach (var item in cartItems)
            {
                var itemCarrinho = new CarrinhoItemRedisDTO();
                itemCarrinho.Id = item.Id;
                itemCarrinho.CarrinhoId = item.CarrinhoId;
                itemCarrinho.ProdutoId = item.ProdutoId;
                itemCarrinho.Produto = item.Produto;
                itemCarrinho.Quantidade = item.Quantidade;
                itemCarrinho.DataInclusao = item.DataInclusao;
                itemCarrinho.DataAlteracao = item.DataAlteracao;
                carrinhoRedis.ItensCarrinho.Add(itemCarrinho);
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            await json.SetAsync($"carrinho:{usuarioId}", "$", JsonSerializer.Serialize(carrinhoRedis, options));
        }

        private async Task RemoverCarrinhoRedis(Guid usuarioId)
        {
            var db = _redisCon.GetDatabase();
            var json = db.JSON();
            await json.DelAsync($"carrinho:{usuarioId}", "$");
        }

        public async Task<IEnumerable<ItemCarrinhoDTO>> GetCarrinhoPorUsuario(Guid usuarioId)
        {
            var db = _redisCon.GetDatabase();
            var json = db.JSON();
            var itensCarrinhoDTO = new List<ItemCarrinhoDTO>();
            var redisValue = await json.GetAsync($"carrinho:{usuarioId}");
            if (string.IsNullOrEmpty(redisValue.ToString()) == false)
            {
                CarrinhoRedisDTO cart = JsonSerializer.Deserialize<CarrinhoRedisDTO>(redisValue.ToString())!;
                List<CarrinhoItemRedisDTO> cartItems = cart.ItensCarrinho!;

                var carrinhoDTO = _mapper.Map<CarrinhoDTO>(cart);
                itensCarrinhoDTO = _mapper.Map<List<ItemCarrinhoDTO>>(cartItems);
                itensCarrinhoDTO.ForEach((item) =>
                {
                    item.Produto.ImagemBase64 = Encoding.UTF8.GetString(cartItems.Find(x => x.Id == item.Id)!.Produto!.Imagem!);
                    item.Carrinho = carrinhoDTO;
                });
            }
            return itensCarrinhoDTO;
        }

        public async Task<bool> InserirCupom(Guid usuarioId, string cupom)
        {
            var carrinho = await con.Carrinho.FirstOrDefaultAsync(x => x.UsuarioId == usuarioId);
            if (carrinho != null)
            {
                carrinho.CodigoCupom = cupom;
                con.Carrinho.Update(carrinho);
                await con.SaveChangesAsync();

                await SalvarCarrinhoRedis(usuarioId);

                return true;
            }
            return false;
        }

        public async Task<bool> LimparCarrinho(Guid usuarioId)
        {
            try
            {
                var carrinho = await con.Carrinho.FirstAsync(x => x.UsuarioId == usuarioId);
                var itensCarrinho = await con.ItensCarrinho.Where(x => x.CarrinhoId == carrinho.Id).ToListAsync();
                con.ItensCarrinho.RemoveRange(itensCarrinho);
                con.Carrinho.Remove(carrinho);
                await con.SaveChangesAsync();
                await RemoverCarrinhoRedis(usuarioId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoverCupom(Guid usuarioId)
        {
            var carrinho = await con.Carrinho.FirstOrDefaultAsync(x => x.UsuarioId == usuarioId);
            if (carrinho != null)
            {
                carrinho.CodigoCupom = "";
                con.Carrinho.Update(carrinho);
                await con.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoverItemDoCarrinho(Guid itemCarrinhoId)
        {
            try
            {
                var itemCarrinho = await con.ItensCarrinho.FirstOrDefaultAsync(x => x.Id == itemCarrinhoId);
                con.ItensCarrinho.Remove(itemCarrinho);
                await con.SaveChangesAsync();
                await SalvarCarrinhoRedis(itemCarrinho.Carrinho.UsuarioId);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}

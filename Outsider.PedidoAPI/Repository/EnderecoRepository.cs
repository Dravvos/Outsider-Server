using Microsoft.EntityFrameworkCore;
using Outsider.PedidoAPI.Model;
using Outsider.PedidoAPI.Model.Context;
using Outsider.DTO;
using Outside.PedidoAPI.Model;

namespace Outsider.PedidoAPI.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly DbContextOptions<OutsiderContext> _con;

        public EnderecoRepository(DbContextOptions<OutsiderContext> con)
        {
            _con = con;
        }
        public async Task AddEndereco(EnderecoModel model)
        {
            using var con = new OutsiderContext(_con);
            
            await con.Endereco.AddAsync(model);
            await con.SaveChangesAsync();
        }

        public async Task DeleteEndereco(Guid id)
        {
            using var con = new OutsiderContext(_con);
            var model = await con.Endereco.FirstAsync(x => x.Id == id);
            con.Endereco.Remove(model);
            await con.SaveChangesAsync();
        }

        public async Task UpdateEndereco(EnderecoModel dto)
        {
            using var con = new OutsiderContext(_con);
            var model = await con.Endereco.FirstAsync(x => x.Id == dto.Id);

            model.CEP = dto.CEP;
            model.Logradouro = dto.Logradouro;
            model.Cidade = dto.Cidade;
            model.Bairro = dto.Bairro;
            model.Complemento = dto.Complemento;
            model.Estado = dto.Estado;
            model.Numero = dto.Numero;
            model.UsuarioId = dto.UsuarioId;
            model.DataAlteracao = dto.DataAlteracao;
            model.Recebedor = dto.Recebedor;

            await con.SaveChangesAsync();
        }
    }
}

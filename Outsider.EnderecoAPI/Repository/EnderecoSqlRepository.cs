using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outside.EnderecoAPI.Model;
using Outside.EnderecoAPI.Model.Context;
using Outsider.DTO;
using Outsider.EnderecoAPI.Model;
using Outsider.EnderecoAPI.Model.Context;

namespace Outsider.EnderecoAPI.Repository
{
    public class EnderecoSqlRepository : IEnderecoSqlRepository
    {
        private readonly IMapper _mapper;
        private readonly OutsiderContext con;

        public EnderecoSqlRepository(IMapper mapper, OutsiderContext outsiderContext)
        {
            _mapper = mapper;
            con = outsiderContext;
        }

        public async Task AddEndereco(EnderecoDTO dto)
        {
            var model = _mapper.Map<EnderecoModel>(dto);
            await con.AddAsync(model);
            await con.SaveChangesAsync();
        }

        public async Task DeleteEndereco(Guid id)
        {
            var model = await con.Enderecos.FirstAsync(x => x.Id == id);
            con.Enderecos.Remove(model);
            await con.SaveChangesAsync();
        }

        public async Task<IEnumerable<EnderecoDTO>> GetAll(Guid usuarioId)
        {
            var enderecos = await con.Enderecos.Where(x => x.UsuarioId == usuarioId).ToListAsync();
            return _mapper.Map<List<EnderecoDTO>>(enderecos);
        }

        public async Task<EnderecoDTO> GetById(Guid id)
        {
            var endereco = await con.Enderecos.FirstAsync(x => x.Id == id);
            return _mapper.Map<EnderecoDTO>(endereco);
        }

        public async Task UpdateEndereco(EnderecoDTO dto)
        {
            var model = await con.Enderecos.FirstAsync(x => x.Id == dto.Id);

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

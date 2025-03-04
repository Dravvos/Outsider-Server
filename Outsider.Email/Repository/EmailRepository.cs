using Microsoft.EntityFrameworkCore;
using Outsider.Email.Mensagens;
using Outsider.Email.Model;
using Outsider.Email.Model.Context;

namespace Outsider.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<OutsiderContext> _con;

        public EmailRepository(DbContextOptions<OutsiderContext> con)
        {
            _con = con;
        }

        public async Task LogEmail(AtualizaResultadoPagamentoMessage message)
        {
            var log = new EmailLog
            {
                DataInclusao = DateTime.Now,
                DataEnvio = DateTime.Now,
                Email = message.Email,
                Log = $"Pedido - {message.PedidoId} criado com sucesso"
            };
            await using var db = new OutsiderContext(_con);
            db.Logs.Add(log);
            await db.SaveChangesAsync();
        }
    }
}

using Outsider.DTO;
using Outsider.Email.Mensagens;
using Outsider.Email.Model;

namespace Outsider.Email.Repository
{
    public interface IEmailRepository
    {
        Task LogEmail(AtualizaResultadoPagamentoMessage message);

    }
}

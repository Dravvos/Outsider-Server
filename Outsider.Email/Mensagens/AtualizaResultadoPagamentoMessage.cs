using Outsider.MessageBus;

namespace Outsider.Email.Mensagens
{
    public class AtualizaResultadoPagamentoMessage:BaseMessage
    {
        public Guid PedidoId { get; set; }
        public bool Pago { get; set; }
        public string? Email { get; set; }
    }
}

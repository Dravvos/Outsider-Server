using Outsider.MessageBus;

namespace Outsider.PedidoAPI.DTO
{
    public class AtualizaResultadoPagamentoDTO
    {
        public Guid PedidoId { get; set; }
        public bool Pago { get; set; }
        public string? Email { get; set; }
    }
}

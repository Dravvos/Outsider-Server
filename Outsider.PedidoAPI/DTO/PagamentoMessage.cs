using Outsider.MessageBus;

namespace Outsider.PedidoAPI.DTO
{
    public class PagamentoMessage:BaseMessage
    {
        public Guid PedidoId { get; set; }
        public string? Nome { get; set; }
        public float ValorCompra {  get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
    }
}

using Outsider.MessageBus;

namespace Outsider.PedidoAPI.Mensagens
{
    public class PagamentoMessage : BaseMessage
    {
        public Guid PedidoId { get; set; }
        public string? Nome { get; set; }
        public float ValorCompra { get; set; }
        public string? Email { get; set; }
        public Guid CarrinhoId { get; set; }
    }
}

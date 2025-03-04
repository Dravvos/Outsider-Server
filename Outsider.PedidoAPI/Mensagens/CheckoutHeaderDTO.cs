using Outsider.DTO;
using Outsider.MessageBus;

namespace Outsider.PedidoAPI.Mensagens
{
    public class CheckoutHeaderDTO : BaseMessage
    {
        public Guid EnderecoId { get; set; }
        public Guid UsuarioId { get; set; }
        public string? CodigoCupom { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public float ValorCompra { get; set; }
        public float ValorDesconto { get; set; }

        public int QuantidadeItens { get; set; }
        public IEnumerable<ItemCarrinhoDTO>? ItensCarrinho { get; set; }
    }
}

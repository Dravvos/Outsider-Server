using Outsider.MessageBus;

namespace Outsider.PedidoAPI.Mensagens
{
    public class EnderecoMessage:BaseMessage
    {
        public string? Recebedor { get; set; }
        public Guid UsuarioId { get; set; }
        public string? Logradouro { get; set; }
        public string? Bairro { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public short Numero { get; set; }
        public string? Complemento { get; set; }
        public string? CEP { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}

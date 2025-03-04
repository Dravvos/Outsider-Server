using Outsider.MessageBus;

namespace Outsider.EnderecoAPI.Mensagens
{
    public class EnderecoMessage:BaseMessage
    {
        public Guid UsuarioId { get; set; }
        public string? Logradouro { get; set; }
        public string? Bairro { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public short Numero { get; set; }
        public string? Complemento { get; set; }
        public string? CEP { get; set; }
        public string? Recebedor { get; set; }
    }
}

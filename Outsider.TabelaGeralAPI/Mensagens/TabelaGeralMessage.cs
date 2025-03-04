using Outsider.MessageBus;

namespace Outsider.TabelaGeralAPI.Mensagens
{
    public class TabelaGeralMessage:BaseMessage
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }

    }
}

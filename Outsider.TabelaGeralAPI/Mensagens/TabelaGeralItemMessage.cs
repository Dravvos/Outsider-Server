using Outsider.MessageBus;

namespace Outsider.TabelaGeralAPI.Mensagens
{
    public class TabelaGeralItemMessage:BaseMessage
    {
        public string? Descricao { get; set; }
        public string? Sigla { get; set; }
        public Guid TabelaGeralId { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public TabelaGeralMessage TabelaGeral { get; set; }
    }


}

using Outsider.MessageBus;

namespace Outsider.ProdutoAPI.Mensagens
{
    public class TabelaGeralItemMessage:BaseMessage
    {
        public string? Descricao { get; set; }
        public string? Sigla { get; set; }
        public Guid TabelaGeralId { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DatalAlteracao { get; set; }

        public TabelaGeralMessage TabelaGeral { get; set; }
    }


}

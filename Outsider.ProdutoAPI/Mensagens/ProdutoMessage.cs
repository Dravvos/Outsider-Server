using Outsider.DTO;
using Outsider.MessageBus;
using System.Text;

namespace Outsider.ProdutoAPI.Mensagens
{
    public class ProdutoMessage:BaseMessage
    {
        public string? Nome { get; set; }
        public float Preco { get; set; }
        public string? Descricao { get; set; }
        public Guid IdTGCategoria { get; set; }
        public byte[]? Imagem
        {
            get
            {
                if (string.IsNullOrEmpty(ImagemBase64) == false)
                    return Encoding.UTF8.GetBytes(ImagemBase64);
                else
                    return new byte[] { };
            }
            set { }
        }
        public string? NomeArquivo { get; set; }
        public string? ImagemBase64 { get; set; }
        public float Peso { get; set; }
        public int Estoque { get; set; }
        public string? SKU { get; set; }
        public Guid IdTGCor { get; set; }
        public TabelaGeralItemDTO? Cor { get; set; }
        public TabelaGeralItemDTO? Categoria { get; set; }
        public Guid IdTGTamanho { get; set; }
        public TabelaGeralItemDTO? Tamanho { get; set; }
    }
}

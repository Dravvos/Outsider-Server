using Outsider.PedidoAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.PedidoAPI.Model
{
    [Table("PedidoItem")]
    public class PedidoItemModel : BaseModel
    {
        [Required]
        [Column("PedidoId")]
        public Guid PedidoId { get; set; }

        [Required]
        [Column("ProdutoId")]
        public Guid ProdutoId { get; set; }

        [ForeignKey("PedidoId")]
        public virtual PedidoModel Pedido { get; set; }

        [Required]
        [Range(1, 100)]
        [Column("Quantidade")]
        public int Quantidade { get; set; }

        [Required]
        [Column("NomeProduto")]
        public string? NomeProduto { get; set; }
        
        [Required]
        [Column("Preco")]
        public float Preco {  get; set; }

        [ForeignKey("ProdutoId")]
        public virtual ProdutoModel Produto { get; set; }
    }
}

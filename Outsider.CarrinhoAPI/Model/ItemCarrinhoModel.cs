using Outsider.CarrinhoAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.CarrinhoAPI.Model
{
    [Table("CarrinhoItem")]
    public class ItemCarrinhoModel:BaseModel
    {
        [Required]
        [Column("CarrinhoId")]
        public Guid CarrinhoId { get; set; }

        [Required]
        [Column("ProdutoId")]
        public Guid ProdutoId {  get; set; }

        [ForeignKey("CarrinhoId")]
        public virtual CarrinhoModel Carrinho { get; set; }

        [ForeignKey("ProdutoId")]
        public virtual ProdutoModel Produto { get; set; }

        [Required]
        [Range(1,100)]
        [Column("Quantidade")]
        public int Quantidade { get; set; }
    }
}

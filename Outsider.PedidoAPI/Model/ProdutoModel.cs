using Microsoft.EntityFrameworkCore;
using Outsider.PedidoAPI.Model.Base;
using Outsider.PedidoAPI.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.PedidoAPI.Model
{
    [Table("Produto")]
    public class ProdutoModel : BaseModel
    {
        [Column("Nome")]
        [Required]
        [StringLength(150)]
        public string? Nome { get; set; }
        
        [Column("Preco")]
        [Required]
        [Range(1.00, 100000.99)]
        public float Preco { get; set; }

        [Column("Descricao")]
        [StringLength(10000)]
        public string? Descricao { get; set; }

        [Column("Imagem")]
        [Required]
        public byte[]? Imagem { get; set; }

        [Required]
        [Range(0.000000, 100.000000)]
        [Column("Peso")]
        public float Peso { get; set; }

        [Required]
        [Range(0,1000000)]        
        [Column("Estoque")]
        public int Estoque { get; set; }

        [Required]
        [StringLength(30)]
        [Column("SKU")]
        public string? SKU { get; set; }

        [Column("IdTGCategoria")]
        [Required]
        
        public Guid IdTGCategoria { get; set; }

        [Column("IdTGCor")]
        [Required]
        
        public Guid IdTGCor { get; set; }

        [Column("IdTGTamanho")]
        [Required]        
        public Guid IdTGTamanho { get; set; }


        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("IdTGCategoria")]
        public virtual TabelaGeralItemModel? Categoria { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("IdTGTamanho")]
        public virtual TabelaGeralItemModel? Tamanho { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("IdTGCor")]
        public virtual TabelaGeralItemModel? Cor { get; set; }

    }
}

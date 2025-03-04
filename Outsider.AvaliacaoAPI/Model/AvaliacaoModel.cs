using Outsider.AvaliacaoAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.AvaliacaoAPI.Model
{
    [Table("Avaliacao")]
    public class AvaliacaoModel:BaseModel
    {

        [Required]        
        [Column("ProdutoId")]
        public Guid ProdutoId { get; set; }

        [Required]
        [Column("UsuarioId")]
        public Guid UsuarioId { get; set; }

        [Required]
        [Range(1,5)]
        [Column("Estrelas")]
        public byte Estrelas { get; set; }

        [StringLength(5000)]
        [Column("Comentario")]
        public string? Comentario { get; set; }

        [Required]
        [Column("DataAvaliacao")]
        public DateTime DataAvaliacao { get; set; }


        [ForeignKey("ProdutoId")]
        public virtual ProdutoModel Produto { get; set; }
    }
}

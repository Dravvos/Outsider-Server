using Outsider.CarrinhoAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.CarrinhoAPI.Model
{
    [Table("TabelaGeral")]
    public class TabelaGeralModel:BaseModel
    {
        [Column("Nome")]
        [StringLength(100)]
        [Required]
        public string? Nome { get; set; }
        
        [Column("Descricao")]
        [StringLength(250)]
        [Required]
        public string? Descricao { get; set; }

    }
}

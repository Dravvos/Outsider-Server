using Outsider.CupomAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.CupomAPI.Model
{
    [Table("Cupom")]
    public class CupomModel : BaseModel
    {
        [Required]
        [Column("CodigoCupom")]
        [StringLength(30)]
        public string? CodigoCupom { get; set; }

        [Required]
        [Column("PorcentagemDesconto")]
        public float PorcentagemDesconto { get; set; }

        [Required]
        [Column("DataValidade")]
        public DateTime DataValidade { get; set; }

    }
}

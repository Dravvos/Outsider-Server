using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outside.PagamentoAPI.Model.Base
{
    public class BaseModel
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        
        [Required]
        [Column("DataInclusao")]
        public DateTime DataInclusao { get; set; }
        
        [Column("DataAlteracao")]
        public DateTime? DataAlteracao { get; set; }
    }
}

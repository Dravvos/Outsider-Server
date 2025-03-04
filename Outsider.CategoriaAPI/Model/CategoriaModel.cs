using Outsider.CategoriaAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.CategoriaAPI.Model
{
    [Table("Categoria")]
    public class CategoriaModel:BaseModel
    {
        [Required]
        [StringLength(100)]
        [Column("Nome")]
        public string? Nome { get; set; }
    }
}

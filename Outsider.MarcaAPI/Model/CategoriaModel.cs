using Outsider.MarcaAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.MarcaAPI.Model
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

using Microsoft.EntityFrameworkCore;
using Outsider.MarcaAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.MarcaAPI.Model
{
    [Table("Marcas")]
    public class MarcaModel:BaseModel
    {
        [Required]
        [StringLength(200)]
        [Column("Nome")]
        public string? Nome { get; set; }

        [Required]        
        [Column("CategoriaId")]
        public Guid CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual CategoriaModel? Categoria { get; set; }

    }

    

}

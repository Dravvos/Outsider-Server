using Outsider.CarrinhoAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.CarrinhoAPI.Model
{
    [Table("Carrinho")]
    public class CarrinhoModel:BaseModel
    {
        [Required]
        [Column("UsuarioId")]
        public Guid UsuarioId { get; set; }

        [Column("CodigoCupom")]
        public string? CodigoCupom { get; set; }
    }
}

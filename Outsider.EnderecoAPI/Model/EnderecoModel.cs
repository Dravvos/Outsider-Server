using Outsider.EnderecoAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outside.EnderecoAPI.Model
{
    [Table("Endereco")]
    public class EnderecoModel:BaseModel
    {
        [Required]
        [StringLength(500)]
        [Column("Recebedor")]
        public string? Recebedor { get; set; }

        [Required]
        [StringLength(9)]
        [Column("CEP")]
        public string? CEP { get; set; }

        [Required]
        [StringLength(25)]
        [Column("Estado")]
        public string? Estado { get; set; }

        [Required]
        [StringLength(300)]
        [Column("Cidade")]
        public string? Cidade { get; set; }

        [Required]
        [StringLength(300)]
        [Column("Bairro")]
        public string? Bairro { get; set; }

        [Required]
        [StringLength(400)]
        [Column("Logradouro")]
        public string? Logradouro { get; set; }

        [Required]
        [Range(1, 100000)]
        [Column("Numero")]
        public int Numero { get; set; }

        [StringLength(500)]
        [Column("Complemento")]
        public string? Complemento { get; set; }
        
        [Required]
        [Column("UsuarioId")]
        public Guid UsuarioId { get; set; }
    }
}

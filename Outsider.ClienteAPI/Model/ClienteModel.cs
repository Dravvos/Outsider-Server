using Outsider.ClienteAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outside.ClienteAPI.Model
{
    [Table("Cliente")]
    public class ClienteModel : BaseModel
    {
        [Required]
        [StringLength(100)]
        [Column("Nome")]
        public string? Nome { get; set; }
        
        [Required]
        [StringLength(500)]
        [Column("Sobrenome")]
        public string? Sobrenome { get; set; }

        [StringLength(14)]
        [Column("CPF")]
        public string? Cpf { get; set; }

        [StringLength(20)]
        [Column("CNPJ")]
        public string? Cnpj { get; set; }

        [Required]
        [StringLength(250)]
        [Column("Email")]
        public string? Email { get; set; }

        [StringLength(20)]
        [Column("Telefone")]
        public string? Telefone { get; set; }

    }
}

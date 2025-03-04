using Outside.PedidoAPI.Model;
using Outsider.DTO;
using Outsider.PedidoAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.PedidoAPI.Model
{
    [Table("Pedido")]
    public class PedidoModel : BaseModel
    {
        [Required]
        [Column("UsuarioId")]
        public Guid UsuarioId { get; set; }        

        [Column("CodigoCupom")]
        public string? CodigoCupom { get; set; }

        [Required]
        [Column("Nome")]
        public string? Nome { get; set; }

        [Required]
        [Column("Sobrenome")]
        public string? Sobrenome { get; set; }

        [Required]
        [Column("DataCompra")]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required]
        [Column("DataPedido")]
        public DateTime DataPedido { get; set; }

        [Required]
        [Column("EnderecoId")]
        public Guid EnderecoId { get; set; }

        [Required]
        [Column("Telefone")]
        public string? Telefone { get; set; }

        [Required]
        [Column("Email")]
        public string? Email { get; set; }

        [Required]
        [Column("ValorCompra")]
        public float ValorCompra { get; set; }

        [Required]
        [Column("ValorDesconto")]
        public float ValorDesconto { get; set; }

        [Required]
        [Column("QuantidadeItens")]
        public int QuantidadeItens { get; set; }

        public List<PedidoItemModel>? ItensPedido { get; set; }

        [Required]
        [Column("PedidoPago")]
        public bool Pago { get; set; }

        [ForeignKey("EnderecoId")]
        public virtual EnderecoModel Endereco { get; set; }
    }
}

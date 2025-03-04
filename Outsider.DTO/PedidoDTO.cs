using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsider.DTO
{
    public class PedidoDTO:BaseDTO
    {
        public Guid UsuarioId { get; set; }
        public string? CodigoCupom { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataPedido { get; set; }
        public Guid EnderecoId { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public float ValorCompra { get; set; }
        public float ValorDesconto { get; set; }
        public int QuantidadeItens { get; set; }
        public bool Pago { get; set; }
    }
}

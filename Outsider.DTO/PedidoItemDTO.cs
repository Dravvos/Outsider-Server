using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsider.DTO
{
    public class PedidoItemDTO:BaseDTO
    {
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public virtual PedidoDTO Pedido { get; set; }
        public int Quantidade { get; set; }
        public string? NomeProduto { get; set; }
        public float Preco { get; set; }
        public virtual ProdutoDTO Produto { get; set; }
    }
}

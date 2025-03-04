using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsider.DTO
{
    public class ItemCarrinhoDTO:BaseDTO
    {
        public Guid CarrinhoId { get; set; }
        public Guid ProdutoId { get; set; }
        public virtual CarrinhoDTO Carrinho { get; set; }
        public virtual ProdutoDTO Produto { get; set; }
        public int Quantidade { get; set; }
    }
}

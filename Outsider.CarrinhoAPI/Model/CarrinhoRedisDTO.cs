using Outsider.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.CarrinhoAPI.Model
{
    [NotMapped]
    public class CarrinhoRedisDTO
    {
        public Guid Id { get; set; }        
        public Guid UsuarioId { get; set; }
        public string? CodigoCupom { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public List<CarrinhoItemRedisDTO>? ItensCarrinho { get; set; }
    }

    [NotMapped]
    public class CarrinhoItemRedisDTO
    {
        public Guid Id { get; set; }
        public Guid CarrinhoId { get; set; }
        public Guid ProdutoId { get; set; }
        public virtual ProdutoModel? Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}

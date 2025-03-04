namespace Outsider.CarrinhoAPI.Model
{
    public class CarrinhoItem
    {
        public Guid UsuarioId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}

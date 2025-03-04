namespace Outsider.Pagamentos
{
    public interface IProcessoPagamento
    {
        string CriarPagamento(float valor);
    }
}

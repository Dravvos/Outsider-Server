using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Outsider.DTO
{
    public class ClienteDTO : BaseDTO
    {
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public string? Email { get; set; }
        public byte[]? Hash { get; set; }
        public byte[]? Salt { get; set; }
        public DateTime? DataUltimoLogin { get; set; }
        public string? Telefone { get; set; }
    }
}

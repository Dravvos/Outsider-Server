using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsider.DTO
{
    public class CarrinhoDTO:BaseDTO
    {
        public Guid UsuarioId { get; set; }
        public string? CodigoCupom { get; set; }
    }
}

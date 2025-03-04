using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsider.DTO
{
    public class CupomDTO : BaseDTO
    {
        public string? CodigoCupom { get; set; }
        public float PorcentagemDesconto { get; set; }
        public DateTime DataValidade { get; set; }
    }
}

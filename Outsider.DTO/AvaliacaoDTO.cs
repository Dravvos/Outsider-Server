using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsider.DTO
{
    public class AvaliacaoDTO:BaseDTO
    {
        public Guid ProdutoId { get; set; }
        public Guid UsuarioId { get; set; }
        public byte Estrelas { get; set; }
        public string? Comentario { get; set; }
        public DateTime DataAvaliacao { get; set; }
    }
}

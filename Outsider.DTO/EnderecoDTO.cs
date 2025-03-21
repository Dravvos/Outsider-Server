﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsider.DTO
{
    public class EnderecoDTO:BaseDTO
    {
        public Guid UsuarioId { get; set; }
        public string? Logradouro { get; set; }
        public string? Bairro { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public short Numero { get; set; }
        public string? Complemento { get; set; }
        public string? CEP { get; set; }
        public string? Recebedor { get; set; }
    }
}

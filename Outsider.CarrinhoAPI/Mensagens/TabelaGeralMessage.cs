﻿using Outsider.MessageBus;

namespace Outsider.CarrinhoAPI.Mensagens
{
    public class TabelaGeralMessage:BaseMessage
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DatalAlteracao { get; set; }
    }
}

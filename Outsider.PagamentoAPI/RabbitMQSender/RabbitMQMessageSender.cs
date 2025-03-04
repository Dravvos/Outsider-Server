using Outsider.PedidoAPI.Mensagens;
using Outsider.MessageBus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Outsider.PagamentoAPI.Mensagens;

namespace Outsider.PagamentoAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private IConnection _connection;
        private readonly IConfiguration _conf;
        private const string _nomeExchange = "DirectAtualizaPagamentoExchange";
        private const string _filaPagamentoEnvioEmail = "filaPagamentoEnvioEmail";
        private const string _filaPedidoEnvioEmail = "filaPedidoEnvioEmail";

        public RabbitMQMessageSender(IConfiguration conf)
        {
            _conf = conf;
        }

        public void SendMessage(BaseMessage message)
        {
            if (ConexaoExiste())
            {
                using var channel = _connection.CreateChannelAsync().Result;
                channel.ExchangeDeclareAsync(_nomeExchange, ExchangeType.Direct, false, false, arguments: null).Wait();
                
                channel.QueueDeclareAsync(_filaPagamentoEnvioEmail, false, false, false, null).Wait();
                channel.QueueDeclareAsync(_filaPedidoEnvioEmail, false,false,false,null).Wait();

                channel.QueueBindAsync(_filaPagamentoEnvioEmail, _nomeExchange, "EmailPagamento");
                channel.QueueBindAsync(_filaPedidoEnvioEmail, _nomeExchange, "EmailPedido");

                byte[] body = GetMessageAsByteArray(message);

                channel.BasicPublishAsync(_nomeExchange, "EmailPagamento", false, body: body);
                channel.BasicPublishAsync(_nomeExchange, "EmailPedido", false, body: body);
            }
        }

        public byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<AtualizaResultadoPagamentoMessage>((AtualizaResultadoPagamentoMessage)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }

        private void CriarConexao()
        {
            try
            {
                var factory = new ConnectionFactory();
                factory.Uri = new Uri(_conf["RabbitMQConnection"]!);
                _connection = factory.CreateConnectionAsync().Result;
            }
            catch (Exception ex)
            {

            }
        }
        private bool ConexaoExiste()
        {
            if (_connection != null)
                return true;
            CriarConexao();
            return _connection != null;
        }

    }
}

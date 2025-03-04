using Outsider.EnderecoAPI.Mensagens;
using Outsider.MessageBus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Outsider.EnderecoAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private IConnection _connection;
        private readonly IConfiguration _conf;
        private const string ExchangeName = "DirectEndereco_Exchange";

        public RabbitMQMessageSender(IConfiguration conf)
        {
            _conf = conf;
        }

        public void SendMessage(BaseMessage message)
        {
            if (ConexaoExiste())
            {
                using var channel = _connection.CreateChannelAsync().Result;
                channel.ExchangeDeclareAsync(ExchangeName,ExchangeType.Direct,false,false,null).Wait();
                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublishAsync(ExchangeName, "", false, body: body);
            }
        }

        public byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<EnderecoMessage>((EnderecoMessage)message, options);
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
            catch(Exception ex)
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

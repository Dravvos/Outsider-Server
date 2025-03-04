using Outsider.CarrinhoAPI.Mensagens;
using Outsider.MessageBus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Outsider.CarrinhoAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private IConnection _connection;

        public void SendMessage(BaseMessage message, string nomeFila, IConfiguration conf)
        {
            if (ConexaoExiste(conf))
            {
                using var channel = _connection.CreateChannelAsync().Result;
                channel.QueueDeclareAsync(queue: nomeFila, false, false, false, arguments: null).Wait();
                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublishAsync("", nomeFila, false, body: body);
            }
        }

        public byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<CheckoutHeaderMessage>((CheckoutHeaderMessage)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }

        private void CriarConexao(IConfiguration conf)
        {
            try
            {
                var factory = new ConnectionFactory();
                factory.Uri = new Uri(conf["RabbitMQConnection"]!);

                _connection = factory.CreateConnectionAsync().Result;
            }
            catch(Exception ex)
            {

            }
        }
        private bool ConexaoExiste(IConfiguration conf)
        {
            if (_connection != null)
                return true;
            CriarConexao(conf);
            return _connection != null;
        }

    }
}

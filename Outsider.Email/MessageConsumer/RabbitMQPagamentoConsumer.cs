using Outsider.Email.Mensagens;
using Outsider.Email.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Outsider.Email.MessageConsumer
{
    public class RabbitMQPagamentoConsumer : BackgroundService
    {
        private readonly EmailRepository _emailRepository;
        private IConnection _connection;
        private IChannel _channel;
        private const string _nomeExchange = "DirectAtualizaPagamentoExchange";
        private const string _filaPagamentoEnvioEmail = "filaPagamentoEnvioEmail";
        

        public RabbitMQPagamentoConsumer(EmailRepository emailRepository, IConfiguration conf)
        {
            _emailRepository = emailRepository;
            var factory = new ConnectionFactory
            {
                Uri = new Uri(conf["RabbitMQConnection"]!),
            };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
            _channel.ExchangeDeclareAsync(_nomeExchange, ExchangeType.Direct, false, false, arguments: null).Wait();
            _channel.QueueDeclareAsync(_filaPagamentoEnvioEmail, false, false, false, null).Wait();
            _channel.QueueBindAsync(_filaPagamentoEnvioEmail, _nomeExchange, "EmailPagamento");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var message = JsonSerializer.Deserialize<AtualizaResultadoPagamentoMessage>(content);
                ProcessarLogs(message).GetAwaiter().GetResult();
                _channel.BasicAckAsync(evt.DeliveryTag, false);
                return Task.CompletedTask;
            };
            _channel.BasicConsumeAsync(_filaPagamentoEnvioEmail, false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessarLogs(AtualizaResultadoPagamentoMessage message)
        {
            try
            {
                await _emailRepository.LogEmail(message);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

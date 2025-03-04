using Outsider.PagamentoAPI.Mensagens;
using Outsider.PagamentoAPI.RabbitMQSender;
using Outsider.Pagamentos;
using Outsider.PedidoAPI.Mensagens;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackExchange.Redis;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;

namespace Outsider.PagamentoAPI.MessageConsumer
{
    public class RabbitMQPagamentoConsumer : BackgroundService
    {
        private IConnection _connection;
        private IChannel _channel;
        private readonly IProcessoPagamento _processoPagamento;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly IConnectionMultiplexer _redisCon;

        public RabbitMQPagamentoConsumer(IProcessoPagamento processoPagamento, IConfiguration conf, IRabbitMQMessageSender rabbitMQMessageSender, IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _processoPagamento = processoPagamento;
            var factory = new ConnectionFactory
            {
                Uri = new Uri(conf["RabbitMQConnection"]!),
            };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
            _channel.QueueDeclareAsync(queue: "processamentoPagamentoFila", false, false, false, arguments: null).Wait();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var dto = JsonSerializer.Deserialize<PagamentoMessage>(content);
                ProcessarPagamento(dto).GetAwaiter().GetResult();
                _channel.BasicAckAsync(evt.DeliveryTag, false);
                return Task.CompletedTask;
            };
            _channel.BasicConsumeAsync("processamentoPagamentoFila", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessarPagamento(PagamentoMessage message)
        {
            var db = _redisCon.GetDatabase();
            var statusResult = new AtualizaResultadoPagamentoMessage
            {
                Pago = await db.KeyDeleteAsync($"Pagamento:{message.CarrinhoId}"),
                PedidoId = message.PedidoId,
                Email = message.Email,
            };
            try
            {
                _rabbitMQMessageSender.SendMessage(statusResult);
                
            }
            catch (Exception ex)
            {


            }
        }
    }
}


using Outsider.PedidoAPI.DTO;
using Outsider.PedidoAPI.Mensagens;
using Outsider.PedidoAPI.Model;
using Outsider.PedidoAPI.RabbitMQSender;
using Outsider.PedidoAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Outsider.PedidoAPI.MessageConsumer
{
    public class RabbitMQPagamentoConsumer : BackgroundService
    {
        private readonly PedidoRepository _pedidoRepository;
        private IConnection _connection;
        private IChannel _channel;
        private const string _nomeExchange = "DirectAtualizaPagamentoExchange";
        private const string _filaPedidoEnvioEmail = "filaPedidoEnvioEmail";

        public RabbitMQPagamentoConsumer(PedidoRepository pedidoRepository, IConfiguration conf)
        {
            _pedidoRepository = pedidoRepository;
            var factory = new ConnectionFactory
            {
                Uri = new Uri(conf["RabbitMQConnection"]!),
            };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
            _channel.ExchangeDeclareAsync(_nomeExchange, ExchangeType.Direct, false, false, arguments: null).Wait();
            _channel.QueueDeclareAsync(_filaPedidoEnvioEmail, false, false, false, null).Wait();
            _channel.QueueBindAsync(_filaPedidoEnvioEmail, _nomeExchange, "EmailPedido");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var dto = JsonSerializer.Deserialize<AtualizaResultadoPagamentoDTO>(content);
                AtualizarStatusPagamento(dto).GetAwaiter().GetResult();
                _channel.BasicAckAsync(evt.DeliveryTag, false);
                return Task.CompletedTask;
            };
            _channel.BasicConsumeAsync(_filaPedidoEnvioEmail, false, consumer);
            return Task.CompletedTask;
        }

        private async Task AtualizarStatusPagamento(AtualizaResultadoPagamentoDTO dto)
        {
            try
            {
                await _pedidoRepository.AtualizarStatusPedido(dto.PedidoId, dto.Pago);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

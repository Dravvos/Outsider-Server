using Outsider.PedidoAPI.Mensagens;
using Outsider.PedidoAPI.Model;
using Outsider.PedidoAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Outsider.PedidoAPI.MessageConsumer
{
    public class RabbitMQTabelaGeralConsumer : BackgroundService
    {
        private readonly TabelaGeralItemRepository _tabelaGeralItemRepository;
        private IConnection _connection;
        private IChannel _channel;
        private const string ExchangeName = "FanoutTabelaGeral_Exchange";
        private string NomeFila = "";

        public RabbitMQTabelaGeralConsumer(TabelaGeralItemRepository tabelaGeralItemRepository, IConfiguration conf)
        {
            _tabelaGeralItemRepository = tabelaGeralItemRepository;
            var factory = new ConnectionFactory
            {
                Uri = new Uri(conf["RabbitMQConnection"]!),
            };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

            _channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Fanout).Wait();
            NomeFila = _channel.QueueDeclareAsync().Result.QueueName;
            _channel.QueueBindAsync(NomeFila, ExchangeName, "").Wait();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var dto = JsonSerializer.Deserialize<TabelaGeralItemMessage>(content);
                ProcessarTabelaGeral(dto).GetAwaiter().GetResult();
                _channel.BasicAckAsync(evt.DeliveryTag, false);
                return Task.CompletedTask;
            };
            _channel.BasicConsumeAsync(NomeFila, false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessarTabelaGeral(TabelaGeralItemMessage message)
        {
            var tabelaGeralItem = new TabelaGeralItemModel
            {
                Id=message.Id,
                Sigla = message.Sigla,
                Descricao = message.Descricao,
                DataInclusao = message.DataInclusao,
                TabelaGeralId = message.TabelaGeralId,
                DataAlteracao = message.DatalAlteracao
            };
            var json = JsonSerializer.Serialize(message.TabelaGeral);
            tabelaGeralItem.TabelaGeral = JsonSerializer.Deserialize<TabelaGeralModel>(json);
            
            if (message.DatalAlteracao.HasValue == false)
                await _tabelaGeralItemRepository.AddAsync(tabelaGeralItem);
            else
                await _tabelaGeralItemRepository.UpdateAsync(tabelaGeralItem);

        }
    }
}

using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Outsider.CarrinhoAPI.Repository;
using Outsider.CarrinhoAPI.Mensagens;
using Outsider.CarrinhoAPI.Model;

namespace Outsider.CarrinhoAPI.MessageConsumer
{
    public class RabbitMQProdutoConsumer:BackgroundService
    {
        private readonly ProdutoRepository _produtoRepository;
        private IConnection _connection;
        private IChannel _channel;
        private const string ExchangeName = "FanoutProduto_Exchange";
        private string NomeFila = "";

        public RabbitMQProdutoConsumer(ProdutoRepository produtoRepository, IConfiguration conf)
        {
            _produtoRepository = produtoRepository;
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
                var dto = JsonSerializer.Deserialize<ProdutoMessage>(content);
                ProcessarProduto(dto).GetAwaiter().GetResult();
                _channel.BasicAckAsync(evt.DeliveryTag, false);
                return Task.CompletedTask;
            };
            _channel.BasicConsumeAsync(NomeFila, false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessarProduto(ProdutoMessage message)
        {
            var json = JsonSerializer.Serialize(message);
            var model = JsonSerializer.Deserialize<ProdutoModel>(json);
            if (message.DataAlteracao.HasValue == false)
                await _produtoRepository.Create(model);
            else
                await _produtoRepository.Update(model);

        }
    }
}

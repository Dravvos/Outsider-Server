using Outside.PedidoAPI.Model;
using Outsider.PedidoAPI.Mensagens;
using Outsider.PedidoAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Outsider.PedidoAPI.MessageConsumer
{
    public class RabbitMQEnderecoConsumer : BackgroundService
    {
        private readonly EnderecoRepository _enderecoRepository;
        private IConnection _connection;
        private IChannel _channel;
        private const string ExchangeName = "DirectEndereco_Exchange";
        private string NomeFila = "";

        public RabbitMQEnderecoConsumer(EnderecoRepository enderecoRepository, IConfiguration conf)
        {
            _enderecoRepository = enderecoRepository;
            var factory = new ConnectionFactory
            {
                Uri = new Uri(conf["RabbitMQConnection"]!),
            };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

            _channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Direct).Wait();
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
                var dto = JsonSerializer.Deserialize<EnderecoMessage>(content);
                ProcessarEndereco(dto).GetAwaiter().GetResult();
                _channel.BasicAckAsync(evt.DeliveryTag, false);
                return Task.CompletedTask;
            };
            _channel.BasicConsumeAsync(NomeFila, false, consumer);
            return Task.CompletedTask;
        }
        private async Task ProcessarEndereco(EnderecoMessage message)
        {
            var endereco = new EnderecoModel
            {
                Id = message.Id,
                UsuarioId = message.UsuarioId,
                Recebedor = message.Recebedor,
                Logradouro = message.Logradouro,
                Bairro = message.Bairro,
                Estado = message.Estado,
                Cidade = message.Cidade,
                Numero = message.Numero,
                Complemento = message.Complemento,
                CEP = message.CEP,
                DataInclusao = message.DataInclusao,
                DataAlteracao = message.DataAlteracao
            };
            await _enderecoRepository.AddEndereco(endereco);
        }
    }
}

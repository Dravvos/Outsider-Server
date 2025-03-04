
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
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly PedidoRepository _pedidoRepository;
        private readonly ProdutoRepository _produtoRepository;
        private IConnection _connection;
        private IChannel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(PedidoRepository pedidoRepository, IConfiguration conf, IRabbitMQMessageSender rabbitMQMessageSender, ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            var factory = new ConnectionFactory
            {
                Uri = new Uri(conf["RabbitMQConnection"]!),
            };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
            _channel.QueueDeclareAsync(queue: "checkoutFila", false, false, false, arguments: null).Wait();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var dto = JsonSerializer.Deserialize<CheckoutHeaderDTO>(content);
                ProcessarPedido(dto).GetAwaiter().GetResult();
                _channel.BasicAckAsync(evt.DeliveryTag, false);
                return Task.CompletedTask;
            };
            _channel.BasicConsumeAsync("checkoutFila", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessarPedido(CheckoutHeaderDTO dto)
        {
            var pedido = new PedidoModel
            {
                UsuarioId = dto.UsuarioId,
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                ItensPedido = new List<PedidoItemModel>(),
                CodigoCupom = dto.CodigoCupom,
                Email = dto.Email,
                Telefone = dto.Telefone,
                ValorCompra = dto.ValorCompra,
                ValorDesconto = dto.ValorDesconto,
                Data = dto.Data,
                DataPedido = DateTime.Now,
                Pago = false,
                DataInclusao = DateTime.Now,
                EnderecoId = dto.EnderecoId
            };

            foreach (var item in dto.ItensCarrinho)
            {
                PedidoItemModel itemPedido = new()
                {
                    ProdutoId = item.ProdutoId,
                    NomeProduto = item.Produto.Nome,
                    Preco = item.Produto.Preco,
                    Quantidade = item.Quantidade,
                    DataInclusao = DateTime.Now
                };
                pedido.QuantidadeItens += itemPedido.Quantidade;
                pedido.ItensPedido.Add(itemPedido);
            }
            await _pedidoRepository.AdicionarPedido(pedido);

            foreach (var item in pedido.ItensPedido)
            {
                var produto = await _produtoRepository.GetById(item.ProdutoId);
                produto.Estoque -= item.Quantidade;
                produto.DataAlteracao = DateTime.Now;
                await _produtoRepository.Update(produto);
            }

            var pagamento = new PagamentoMessage
            {
                Nome = pedido.Nome + " " + pedido.Sobrenome,
                PedidoId = pedido.Id,
                ValorCompra = pedido.ValorCompra,
                Email = pedido.Email,
                Telefone = pedido.Telefone
            };
            try
            {
                _rabbitMQMessageSender.SendMessage(pagamento, "processamentoPagamentoFila");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

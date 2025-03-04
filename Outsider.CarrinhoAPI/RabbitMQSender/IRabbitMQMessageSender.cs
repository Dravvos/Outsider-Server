using Outsider.MessageBus;

namespace Outsider.CarrinhoAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage data, string nomeFila, IConfiguration conf);

    }
}

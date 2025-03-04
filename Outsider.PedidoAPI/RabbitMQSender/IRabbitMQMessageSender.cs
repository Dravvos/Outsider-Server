using Outsider.MessageBus;

namespace Outsider.PedidoAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage data, string nomeFila);

    }
}

using Outsider.MessageBus;

namespace Outsider.ProdutoAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage data);

    }
}

using Outsider.MessageBus;

namespace Outsider.PagamentoAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage data);

    }
}

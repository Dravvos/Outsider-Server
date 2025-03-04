using Outsider.MessageBus;

namespace Outsider.EnderecoAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage data);

    }
}

using Outsider.MessageBus;

namespace Outsider.TabelaGeralAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage data);

    }
}

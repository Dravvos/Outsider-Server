﻿namespace Outsider.MessageBus
{
    public interface IMessageBus
    {
        Task PublicMessage(BaseMessage message, string nomeFila);
    }
}

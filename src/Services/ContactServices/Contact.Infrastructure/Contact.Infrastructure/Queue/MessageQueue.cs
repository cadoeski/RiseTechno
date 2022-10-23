using Contact.Application.Contracts.Infrastructure;
using Contracts.Contact;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Queue
{
    public class MessageQueue : IMessageQueue
    {
        private readonly IPublishEndpoint publishEndpoint;
        public MessageQueue(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task Publish(UserCreated userResponse)
        {
            await publishEndpoint.Publish(userResponse);
        }
    }
}

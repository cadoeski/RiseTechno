using Report.Application.Contracts.Infrastructure;
using Contracts.Contact;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Infrastructure.Queue
{
    public class MessageQueue : IMessageQueue
    {
        private readonly IPublishEndpoint publishEndpoint; 
        public MessageQueue(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint)); 
        }

        public async Task Publish(LocationCreated userResponse)
        {
            await publishEndpoint.Publish(userResponse); 
        }

        public async Task Publish(LocationComplated userResponse)
        {
            await publishEndpoint.Publish(userResponse);
        }

    }
}


using Contracts.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Contracts.Infrastructure
{
    public interface IMessageQueue
    {
        Task Publish(LocationCreated  Created);

        Task Publish(LocationComplated Created);
    }
}

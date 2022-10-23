using AutoMapper;
using Contact.Application.Contracts.Infrastructure;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using Contracts.Contact;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Features.Users.UserCreate
{
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand,bool>
    {
        private readonly IUserWriteRepository userWriteRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UserDeleteCommandHandler> logger;
        private readonly IMessageQueue messageQueue;
        public UserDeleteCommandHandler(IUserWriteRepository userWriteRepository, IMapper mapper, ILogger<UserDeleteCommandHandler> logger, IMessageQueue messageQueue)
        {
            this.userWriteRepository = userWriteRepository ?? throw new ArgumentNullException(nameof(userWriteRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger;
            this.messageQueue = messageQueue;
        }

        public async Task<bool> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<Domain.Entities.User>(request);
            var result = await userWriteRepository.RemoveAsync(user.id);
            await userWriteRepository.SaveChanges();

            logger.LogInformation("User silindi {@id}", user.id);

            await messageQueue.Publish(new UserCreated(user.id.ToString(), "rt-user", DateTime.Now));

            return result;

        }




    }
}

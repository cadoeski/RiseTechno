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

    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, User>
    {
        private readonly IUserWriteRepository userWriteRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UserCreateCommandHandler> logger;
        private readonly IMessageQueue messageQueue;
        public UserCreateCommandHandler(IUserWriteRepository userWriteRepository, IMapper mapper, ILogger<UserCreateCommandHandler> logger, IMessageQueue messageQueue)
        {
            this.userWriteRepository = userWriteRepository ?? throw new ArgumentNullException(nameof(userWriteRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger;
            this.messageQueue = messageQueue;
        }

        public async Task<User> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {

            var user = mapper.Map<Domain.Entities.User>(request);
            _ = await userWriteRepository.AddAsync(user);
            await userWriteRepository.SaveChanges();

            logger.LogInformation("Yeni user oluşturuldu {@id}", user.id);

            await messageQueue.Publish(new UserCreated(user.id.ToString(), "rt-user", DateTime.Now));

            return user;


        }
 


    }
}

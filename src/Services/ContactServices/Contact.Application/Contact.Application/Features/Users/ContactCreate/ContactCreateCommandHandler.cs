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

namespace Contact.Application.Features.Users.ContactCreate
{
    public class ContactCreateCommandHandler : IRequestHandler<ContactCreateCommand, contact>
    {
        private readonly IContactWriteRepository contactWriteRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ContactCreateCommandHandler> logger;
        private readonly IMessageQueue messageQueue;
        public ContactCreateCommandHandler(IContactWriteRepository contactWriteRepository, IMapper mapper, ILogger<ContactCreateCommandHandler> logger, IMessageQueue messageQueue)
        {
            this.contactWriteRepository = contactWriteRepository ?? throw new ArgumentNullException(nameof(contactWriteRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger;
            this.messageQueue = messageQueue;
        }

        public async Task<contact> Handle(ContactCreateCommand request, CancellationToken cancellationToken)
        {

            var contact = mapper.Map<Domain.Entities.contact>(request);
            _ = await contactWriteRepository.AddAsync(contact);
            await contactWriteRepository.SaveChanges();

            logger.LogInformation("Yeni contact oluşturuldu {@id}", contact.id);

            await messageQueue.Publish(new ContactCreated(contact.id.ToString(), "rt-user", DateTime.Now));

            return contact;


        }
    }
}

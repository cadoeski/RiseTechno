using Contact.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Features.Users.ContactCreate
{
    public class ContactCreateCommand : IRequest<Domain.Entities.Contact>
    {
        public string phone_number { get; set; }

        public string email { get; set; }

        public string location { get; set; }

        public string userid { get; set; }
    }
}

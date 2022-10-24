using Contact.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Features.Users.UserCreate
{
    public class UserUpdateCommand : IRequest<User>
    {
        public Guid uid { get; set; }
        public string name { get; set; }

        public string surname { get; set; }

        public string company { get; set; }

    }
}

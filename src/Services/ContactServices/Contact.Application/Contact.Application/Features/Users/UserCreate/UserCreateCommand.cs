using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Contact.Domain.Entities;

namespace Contact.Application.Features.Users.UserCreate
{
    public class UserCreateCommand : IRequest<User>
    {
        
        public string name { get; set; }

        public string surname { get; set; }

        public string company { get; set; }

    }
}

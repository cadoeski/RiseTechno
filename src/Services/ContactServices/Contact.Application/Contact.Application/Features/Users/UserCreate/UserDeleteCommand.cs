using Contact.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Features.Users.UserCreate
{
    public class UserDeleteCommand : IRequest<bool>
    {
        public string uid { get; set; }

    }
}

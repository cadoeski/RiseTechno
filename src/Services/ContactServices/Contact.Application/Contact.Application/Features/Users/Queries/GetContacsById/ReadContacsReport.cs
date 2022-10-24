using Contact.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Features.Users.Queries.GetContacsById
{
    public class ReadContacsReport : IRequest<User>
    {
        public Guid Id { get; set; }
        public ReadContacsReport(Guid id)
        {
            Id = id;
        }
    }
}

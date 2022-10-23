using Contact.Domain.Common;
using Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Contracts.Persistence
{
    public interface IUserWriteRepository : IWriteRepository<User>
    {
       
         
    }
}

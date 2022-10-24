using Contact.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Contracts.Persistence
{
    public interface IReadRepository<T> : IRepository<T> where T : EntityBase
    {

        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> GetByIdAsync(Guid id);

    }
}

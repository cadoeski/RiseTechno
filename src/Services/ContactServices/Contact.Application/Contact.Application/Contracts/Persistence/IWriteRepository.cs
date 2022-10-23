using Contact.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Contracts.Persistence
{
    public interface IWriteRepository<T>:IRepository<T> where T : EntityBase
    {
        DbSet<T> Table { get; }

        Task<bool> AddAsync(T entity);

        Task AddRangeAsync(List<T> entities);

        bool Remove(T entity);

        Task<bool> RemoveAsync(Guid id);

        bool Update(T entity);

        Task<bool> UpdateAsync(T entity);

        Task SaveChanges();
    }
}

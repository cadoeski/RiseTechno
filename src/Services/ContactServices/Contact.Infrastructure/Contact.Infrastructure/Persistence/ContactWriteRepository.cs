using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Persistence
{
    public class ContactWriteRepository : IContactWriteRepository
    {
        private readonly IConfiguration configuration;
        private readonly UsersDbContext context;

        public ContactWriteRepository(IConfiguration configuration, UsersDbContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        public DbSet<Domain.Entities.Contact> Table => context.Set<Contact.Domain.Entities.Contact>();
        public async Task<bool> AddAsync(Domain.Entities.Contact entity)
        {
            EntityEntry<Domain.Entities.Contact> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(List<Domain.Entities.Contact> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public bool Remove(Domain.Entities.Contact entity)
        {
            EntityEntry<Domain.Entities.Contact> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            Domain.Entities.Contact entity = await Table.FirstOrDefaultAsync(x => x.id == id);
            return Remove(entity);
        }


        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public bool Update(Domain.Entities.Contact entity)
        {
            EntityEntry<Domain.Entities.Contact> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<bool> UpdateAsync(Domain.Entities.Contact entity)
        {
            EntityEntry<Domain.Entities.Contact> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}

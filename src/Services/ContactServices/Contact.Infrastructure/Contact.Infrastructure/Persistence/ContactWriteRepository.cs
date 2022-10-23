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
        private readonly ContactDbContext context;

        public ContactWriteRepository(IConfiguration configuration, ContactDbContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        public DbSet<Contacts> Table => context.Set<Contacts>();
        public async Task<bool> AddAsync(Contacts entity)
        {
            EntityEntry<Contacts> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(List<Contacts> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public bool Remove(Contacts entity)
        {
            EntityEntry<Contacts> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            Contacts entity = await Table.FirstOrDefaultAsync(x => x.id == id);
            return Remove(entity);
        }


        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public bool Update(Contacts entity)
        {
            EntityEntry<Contacts> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<bool> UpdateAsync(Contacts entity)
        {
            EntityEntry<Contacts> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}

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

        public DbSet<contact> Table => context.Set<contact>();
        public async Task<bool> AddAsync(contact entity)
        {
            EntityEntry<contact> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(List<contact> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public bool Remove(contact entity)
        {
            EntityEntry<contact> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            contact entity = await Table.FirstOrDefaultAsync(x => x.id == id);
            return Remove(entity);
        }


        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public bool Update(contact entity)
        {
            EntityEntry<contact> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<bool> UpdateAsync(contact entity)
        {
            EntityEntry<contact> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}

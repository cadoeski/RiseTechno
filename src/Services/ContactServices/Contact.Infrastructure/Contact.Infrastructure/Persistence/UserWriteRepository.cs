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
    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly IConfiguration configuration;
        private readonly UsersDbContext context;

        public UserWriteRepository(IConfiguration configuration, UsersDbContext dbContext)
        {
            this.configuration = configuration;
            this.context = dbContext;
        }

        public DbSet<User> Table => context.Set<User>();
        public async Task<bool> AddAsync(User entity)
        {
            EntityEntry<User> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(List<User> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public bool Remove(User entity)
        {
            EntityEntry<User> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            User entity = await Table.FirstOrDefaultAsync(x => x.id == id);
            return Remove(entity);
        }


        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public bool Update(User entity)
        {
            EntityEntry<User> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            EntityEntry<User> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}

 
using Report.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Report.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Infrastructure.Persistence
{
    public class LocationStatusReportWriteRepository : ILocationStatusReportWriteRepository
    {
        private readonly IConfiguration configuration;
        private readonly UsersDbContext context;

        public LocationStatusReportWriteRepository(IConfiguration configuration, UsersDbContext dbContext)
        {
            this.configuration = configuration;
            this.context = dbContext;
        }

        public DbSet<LocationStatusReport> Table => context.Set<LocationStatusReport>();
        public async Task<bool> AddAsync(LocationStatusReport entity)
        {
            EntityEntry<LocationStatusReport> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(List<LocationStatusReport> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public bool Remove(LocationStatusReport entity)
        {
            EntityEntry<LocationStatusReport> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            LocationStatusReport entity = await Table.FirstOrDefaultAsync(x => x.id == id);
            return Remove(entity);
        }


        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public bool Update(LocationStatusReport entity)
        {
            EntityEntry<LocationStatusReport> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<bool> UpdateAsync(LocationStatusReport entity)
        {
            EntityEntry<LocationStatusReport> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}

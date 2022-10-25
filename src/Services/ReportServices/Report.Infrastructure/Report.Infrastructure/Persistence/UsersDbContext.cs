using Report.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Infrastructure.Persistence
{
    public class UsersDbContext : DbContext
    {
        private IConfiguration configuration;

        public UsersDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


       
        public DbSet<LocationStatusReport> locationreport { get; set; }

        public DbSet<vw_report> vw_reports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(configuration["Database:Writes"])
            .UseLowerCaseNamingConvention();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
         
          

            #region LocationReport

            modelBuilder.Entity<LocationStatusReport>().Property("id")
         .HasColumnType("uuid")
         .HasDefaultValueSql("gen_random_uuid()")
         .IsRequired();

            #endregion


            #region vw_report

            modelBuilder
          .Entity<vw_report>(eb =>
          {
              eb.HasNoKey();
              eb.ToView("vw_report");
          });

            #endregion


        }

    }
}

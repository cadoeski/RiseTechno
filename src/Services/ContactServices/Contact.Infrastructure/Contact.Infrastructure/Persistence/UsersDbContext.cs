using Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Persistence
{
    public class UsersDbContext : DbContext
    {
        private IConfiguration configuration;

        public UsersDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public DbSet<User> user { get; set; }

        //public DbSet<ContactReport> report { get; set; }

        public DbSet<contact> contact { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(configuration["Database:Writes"])
            .UseLowerCaseNamingConvention();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            #region User




            modelBuilder.Entity<User>().Property("id")
       .HasColumnType("uuid")
       .HasDefaultValueSql("uuid_generate_v4()")
       .IsRequired();

            modelBuilder.Entity<User>()
             .HasIndex(u => u.id)
             .IsUnique();

            #endregion

            #region Report


           // modelBuilder.Entity<contact>().HasOne<ContactReport>().WithMany(p => p.contactList).HasForeignKey(p => p.userid);

            //modelBuilder.Entity<ContactReport>()
            //.HasMany(b => b.contactList)
            //.WithOne().HasForeignKey(p => p.userid);

            #endregion


        }

    }
}

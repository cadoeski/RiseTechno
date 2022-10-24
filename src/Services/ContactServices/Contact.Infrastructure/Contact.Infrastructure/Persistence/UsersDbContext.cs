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
         
        public DbSet<Domain.Entities.Contact> contact { get; set; }

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
       .HasDefaultValueSql("gen_random_uuid()")
       .IsRequired();



            modelBuilder.Entity<User>()
             .HasIndex(u => u.id)
             .IsUnique();


            modelBuilder.Entity<User>()
             .HasMany(b => b.contacts)
             .WithOne();


            #endregion

            #region Contact

            modelBuilder.Entity<Contact.Domain.Entities.Contact>()
        .HasIndex(u => u.id)
        .IsUnique();

            modelBuilder.Entity<Contact.Domain.Entities.Contact>().Property("id")
       .HasColumnType("uuid")
       .HasDefaultValueSql("gen_random_uuid()")
       .IsRequired();



            modelBuilder.Entity<Contact.Domain.Entities.Contact>()
       .HasOne(p => p.user)
       .WithMany(b => b.contacts)
       .HasForeignKey(p => p.userid);

            
            #endregion


        }

    }
}

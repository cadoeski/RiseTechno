using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Contact.Infrastructure.Persistence
{
    public class UserReadRepository : IReadRepository<User>, IUsersReadRepository
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<UserReadRepository> logger;

        public UserReadRepository(IConfiguration configuration, ILogger<UserReadRepository> logger)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("Database:Reads"));

            var results = await connection.QueryAsync<User>
               ("SELECT * FROM public.user");

            return results.ToList();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("Database:Reads"));

            var userDictinory = new Dictionary<Guid, User>();
            string sql = @"select *  FROM public.user u inner join public.contact c on c.userid=u.id WHERE u.id = '" + id.ToString() + "'";
            var contact = await connection.QueryAsync<User, Contact.Domain.Entities.Contact, User>(sql, (u, c) =>
            {
                User userEntry;
                if (!userDictinory.TryGetValue(u.id, out userEntry))
                {
                    userEntry = u;
                    userEntry.contacts = new List<Contact.Domain.Entities.Contact>();
                    userDictinory.Add(u.id, userEntry);
                }

                userEntry.contacts.Add(c);
                return userEntry;
            });

            var user = contact.FirstOrDefault();


            return user;

        }
    }
}

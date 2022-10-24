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
    public  class UserReadRepository  : IReadRepository<ContactReport>, IUsersReadRepository
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<UserReadRepository> logger;

        public UserReadRepository(IConfiguration configuration, ILogger<UserReadRepository> logger)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyList<ContactReport>> GetAllAsync(CancellationToken cancellationToken)
        {
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("Database:Reads"));

            var results = await connection.QueryAsync<ContactReport>
               ("SELECT * FROM public.user");

            return results.ToList();
        }

        public async Task<ContactReport> GetByIdAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("Database:Reads"));

            var contact = await connection.QuerySingleOrDefaultAsync<ContactReport>
                         ("select *  FROM public.user WHERE id = @Id", new { Id = id });

            return contact;

        }
    }
}

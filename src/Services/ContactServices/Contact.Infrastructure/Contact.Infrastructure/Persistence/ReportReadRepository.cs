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
    public class ReportReadRepository : IReportReadRepository
    {
        private readonly IConfiguration configuration;

        public ReportReadRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<List<vw_report>> GetReport()
        {
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("Database:Reads"));

            var results = await connection.QueryAsync<vw_report>("SELECT * FROM public.vw_report");

            return results.ToList();
        }
    }
}

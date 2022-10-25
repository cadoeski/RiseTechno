using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Report.Application.Contracts.Persistence;
using Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Report.Infrastructure.Persistence
{
    public class ReportReadRepository : IReportReadRepository
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ReportReadRepository> logger;

        public ReportReadRepository(IConfiguration configuration, ILogger<ReportReadRepository> logger)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

       

        public async Task<LocationStatusReport> GetReport(Guid id)
        {
            using var connection = new NpgsqlConnection(configuration.GetValue<string>("Database:Reads"));
             
            string sql = @"select *  FROM public.locationreport WHERE  id = '" + id.ToString() + "'";

            var location = await connection.QueryFirstOrDefaultAsync<LocationStatusReport>(sql); 

            return location;

        }
    }
}

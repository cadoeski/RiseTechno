using Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Contracts.Persistence
{
    public interface IReportReadRepository
    {
        Task<LocationStatusReport> GetReport(Guid id);
    }
}

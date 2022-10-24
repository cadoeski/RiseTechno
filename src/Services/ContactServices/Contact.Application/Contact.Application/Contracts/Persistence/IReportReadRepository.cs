using Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Contracts.Persistence
{
    public interface IReportReadRepository
    {
        Task<List<vw_report>> GetReport();
    }
}

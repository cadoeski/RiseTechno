using Report.Domain.Common;
using Report.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Contracts.Persistence
{
    public interface ILocationStatusReportWriteRepository : IWriteRepository<LocationStatusReport>
    {
       
         
    }
}

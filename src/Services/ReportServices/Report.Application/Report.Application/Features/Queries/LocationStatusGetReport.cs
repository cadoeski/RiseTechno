using MediatR;
using Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Features.Queries
{
    public class LocationStatusGetReport : IRequest<LocationStatusReport>
    {
        public Guid reportId { get; set; }
    }
}

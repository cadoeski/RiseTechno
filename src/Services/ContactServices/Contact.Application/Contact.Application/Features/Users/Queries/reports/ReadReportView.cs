using Contact.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Features.Users.Queries.reports
{
    public class ReadReportView : IRequest<List<vw_report>>
    {
        public string username { get; set; }
    }
}

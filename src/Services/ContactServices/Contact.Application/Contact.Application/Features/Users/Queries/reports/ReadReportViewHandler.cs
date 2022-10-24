using AutoMapper;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Features.Users.Queries.reports
{
    public class ReadReportViewHandler : IRequestHandler<ReadReportView, List<vw_report>>
    {
        private readonly IReportReadRepository reportReadRepository;
        private readonly IMapper mapper;

        public ReadReportViewHandler(IReportReadRepository reportReadRepository, IMapper mapper)
        {
            this.reportReadRepository = reportReadRepository ?? throw new ArgumentNullException(nameof(reportReadRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<vw_report>> Handle(ReadReportView request, CancellationToken cancellationToken)
        {
            var musteri = await reportReadRepository.GetReport(); 

            var musteriVm = mapper.Map<List<vw_report>>(musteri);

            return musteriVm;
        } 
    }
}

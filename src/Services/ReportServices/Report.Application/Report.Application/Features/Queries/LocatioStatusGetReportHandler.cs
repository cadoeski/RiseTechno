using AutoMapper;
using ClosedXML.Excel;
using Contracts.Contact;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Report.Application.Contracts.Infrastructure;
using Report.Application.Contracts.Persistence;
using Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace Report.Application.Features.Queries
{
    public class LocatioStatusGetReportHandler : IRequestHandler<LocationStatusGetReport, LocationStatusReport>
    {
        private readonly IReportReadRepository readRepository;
        private readonly IMapper mapper;
        private readonly ILogger<LocatioStatusGetReportHandler> logger;
        private readonly IMessageQueue messageQueue;

        public LocatioStatusGetReportHandler(IReportReadRepository readRepository, IMapper mapper, ILogger<LocatioStatusGetReportHandler> logger, IMessageQueue messageQueue)
        {
            this.readRepository = readRepository ?? throw new ArgumentNullException(nameof(readRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger;
            this.messageQueue = messageQueue;
        }

        public async Task<Domain.Entities.LocationStatusReport> Handle(LocationStatusGetReport request, CancellationToken cancellationToken)
        {
            var location = await readRepository.GetReport(request.reportId);


            var musteriVm = mapper.Map<LocationStatusReport>(location);

            return musteriVm;
        }
    }
}

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

namespace Report.Application.Features.LocationReport
{
    public class LocationStatusReportCommandHandler : IRequestHandler<LocationStatusReportCreateCommand, Domain.Entities.LocationStatusReport>
    {
        private readonly ILocationStatusReportWriteRepository writeRepository;
        private readonly IMapper mapper;
        private readonly ILogger<LocationStatusReportCommandHandler> logger;
        private readonly IMessageQueue messageQueue;
        public LocationStatusReportCommandHandler(ILocationStatusReportWriteRepository writeRepository, IMapper mapper, ILogger<LocationStatusReportCommandHandler> logger, IMessageQueue messageQueue)
        {
            this.writeRepository = writeRepository ?? throw new ArgumentNullException(nameof(writeRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger;
            this.messageQueue = messageQueue;
        }


        public async Task<Domain.Entities.LocationStatusReport> Handle(LocationStatusReportCreateCommand request, CancellationToken cancellationToken)
        {
            //location tablosuna kayıt at
            //Servis sorgula 
            //dönen veriyi excel yap
            //location tablosunu güncelle
            await messageQueue.Publish(new LocationCreated("start", "rt-user", DateTime.Now));

            Domain.Entities.LocationStatusReport locationStatus = new Domain.Entities.LocationStatusReport();
            locationStatus.report = "";
            locationStatus.status = "Hazırlanıyor";
            locationStatus.created_date = locationStatus.created_date.ToUniversalTime();


            var location = await writeRepository.AddAsync(locationStatus);
            await writeRepository.SaveChanges();

            await messageQueue.Publish(new LocationComplated(locationStatus.id.ToString()));





            return locationStatus;

        }





    }
}

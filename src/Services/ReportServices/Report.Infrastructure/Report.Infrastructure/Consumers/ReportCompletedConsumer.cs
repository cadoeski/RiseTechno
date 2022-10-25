using Contracts.Contact;
using MassTransit;
using Microsoft.Extensions.Logging;
using Report.Application.Contracts.Persistence;
using Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Infrastructure.Consumers
{
    public class ReportCompletedConsumer :IConsumer<LocationComplated>
    {
        private readonly ILocationStatusReportWriteRepository writeRepository; 
        private readonly ILogger<ReportCompletedConsumer> logger;

        public ReportCompletedConsumer(ILocationStatusReportWriteRepository writeRepository, ILogger<ReportCompletedConsumer> logger)
        {

            this.writeRepository = writeRepository;
            this.logger = logger; 
        }

        public async Task Consume(ConsumeContext<LocationComplated> context)
        {

            LocationStatusReport report = new LocationStatusReport();
            report.report = context.Message.url;
            report.id =  new Guid(context.Message.id);
            report.status = "Tamamlandı";
             
            var result = await writeRepository.UpdateAsync(report);
            await writeRepository.SaveChanges();

            //await  musteriRepository.AddAsync(musteri);


        }
    }
}

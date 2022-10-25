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


            Domain.Entities.LocationStatusReport locationStatus = new Domain.Entities.LocationStatusReport();
            locationStatus.report = "";
            locationStatus.status = "Hazırlanıyor";
            locationStatus.created_date = locationStatus.created_date.ToUniversalTime();


            var location = await writeRepository.AddAsync(locationStatus);
            await writeRepository.SaveChanges();


            await messageQueue.Publish(new LocationCreated(locationStatus.id.ToString(), "rt-user", DateTime.Now));
            _ = await CreateExcel(locationStatus);

            

            return locationStatus;

        }


        public async Task<bool> CreateExcel(LocationStatusReport location)
        {
            var report = new List<vw_report>();
            using (var httpClient = new HttpClient())
            {


                ReadReportView view = new ReadReportView();
                view.username = "test";

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(view);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                httpClient.BaseAddress = new Uri("https://localhost:7095");
                var response = httpClient.PostAsync("api/v1/Contact/GetLocationReport", content).Result;


                var contents = response.Content.ReadAsStringAsync().Result;
                report = JsonConvert.DeserializeObject<List<vw_report>>(contents);



            }


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report");
                worksheet.Cell(1, 1).Value = "konum";
                worksheet.Cell(1, 2).Value = "kayitli_kisi";
                worksheet.Cell(1, 3).Value = "tel_sayisi";


                int count = 2;
                foreach (var item in report)
                {
                    worksheet.Cell(2, 1).Value = item.konum;
                    worksheet.Cell(2, 2).Value = item.kayitli_kisi;
                    worksheet.Cell(2, 3).Value = item.tel_sayisi;
                    count++;
                }
                string dosyaAdi = "reports/" + Guid.NewGuid().ToString() + ".xlsx";
                workbook.SaveAs(dosyaAdi);

               

                return true;
            }
        }


    }
}

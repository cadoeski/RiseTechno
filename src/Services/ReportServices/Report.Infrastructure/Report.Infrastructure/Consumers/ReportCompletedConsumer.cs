using ClosedXML.Excel;
using Contracts.Contact;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Report.Application.Contracts.Persistence;
using Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Report.Infrastructure.Consumers
{
    public class ReportCompletedConsumer : IConsumer<LocationComplated>
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
            string dosyaAdi = await CreateExcel(context.Message.id);
            report.report = dosyaAdi;
            report.id =  new Guid(context.Message.id);
            report.status = "Tamamlandı";
             
            var result = await writeRepository.UpdateAsync(report);
            await writeRepository.SaveChanges();

            //await  musteriRepository.AddAsync(musteri);


        }


        public async Task<string> CreateExcel(string id)
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

                return dosyaAdi;
            }
        }
    }
}

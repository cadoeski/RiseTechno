using Contracts.Contact;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Report.Application.Features.LocationReport;
using Report.Application.Features.Queries;

namespace Report.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ReportController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> CreateReport(LocationStatusReportCreateCommand user)
        {
            var result = await mediator.Send(user); 

            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> GetReportStatus(LocationStatusGetReport user)
        {
            var result = await mediator.Send(user);

            return Ok(result);
        }
    }
}

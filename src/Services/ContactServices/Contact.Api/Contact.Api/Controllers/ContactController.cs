using Contact.Application.Features.Users.ContactCreate;
using Contact.Application.Features.Users.Queries.GetContacsById;
using Contact.Application.Features.Users.Queries.reports;
using Contact.Application.Features.Users.Response;
using Contact.Application.Features.Users.UserCreate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Contact.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContactController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
        }


        [HttpPost("[action]")]
        [ProducesResponseType(typeof(UserCreateResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UserCreate(UserCreateCommand user)
        {
            var result = await mediator.Send(user); 

            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(UserCreateResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UserUpdate(UserUpdateCommand user)
        {
            var result = await mediator.Send(user);

            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(UserCreateResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UserDelete(UserDeleteCommand user)
        {
            var result = await mediator.Send(user);

            return Ok(result);
        }

        [HttpPost("[action]")] 
        public async Task<ActionResult> ContactCreate(ContactCreateCommand contacts)
        {
            var result = await mediator.Send(contacts);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetUser(ReadContacsReport contacts)
        {
            var result = await mediator.Send(contacts);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetLocationReport(ReadReportView contacts)
        {
            var result = await mediator.Send(contacts);

            return Ok(result);
        }


    }
}

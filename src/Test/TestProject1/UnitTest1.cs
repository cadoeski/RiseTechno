using AutoMapper;
using Contact.Application.Contracts.Infrastructure;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Features.Users.UserCreate;
using Contact.Domain.Entities;
using Contact.Infrastructure.Persistence;
using Contact.Infrastructure.Queue;
using Contracts.Contact;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Contact.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Report.Application.Features.LocationReport;
using Report.Api.Controllers;

namespace TestProject1
{
    public class ContactApiTest
    {
        [InlineData("123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789", "test", "test2")]
        [InlineData("�irket Ad�", "Name", "surName")]
        [InlineData("�irket Ad�", "name", "U")]
        [InlineData("�irket Ad�", "n", "surname")]
        [InlineData("�irket Ad�", "n", "s")]
        [Theory]
        public void CreateUserValidationTest(string c, string n, string s)
        {
            var _sut = new UserCreateCommandValidator();
            UserCreateCommand user = new UserCreateCommand();
            user.company = c;
            user.name = n;
            user.surname = s;

            var validationResult = _sut.Validate(user);

            validationResult.Errors.ToArray().Should().BeEmpty();
        }

        [InlineData("123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789", "test", "test2")]
        [InlineData("�irket Ad�", "Name", "surName")]
        [InlineData("�irket Ad�", "name", "U")]
        [InlineData("�irket Ad�", "n", "surname")]
        [InlineData("�irket Ad�", "n", "s")]
        [Theory]
        public async Task CreateUser_ShouldReturn200(string c, string n, string s)
        {
            /// Arrange
            var servie = new Mock<MediatR.IMediator>();
            var contextAc = new Mock<IHttpContextAccessor>();


            UserCreateCommand user = new UserCreateCommand();
            user.company = c;
            user.name = n;
            user.surname = s;

            var sut = new ContactController(servie.Object, contextAc.Object);

            /// Act
            var result = await sut.UserCreate(user) as OkObjectResult;


            // /// Assert 

            Assert.True(result.StatusCode.Value == 200);
            result.Should().NotBeNull();

        }


        [Fact]

        public async Task GetLocationReport_Count()
        {
            /// Arrange
            var servie = new Mock<MediatR.IMediator>();
            var contextAc = new Mock<IHttpContextAccessor>();


            LocationStatusReportCreateCommand user = new LocationStatusReportCreateCommand();
            user.username = "TEST";

            var sut = new ReportController(servie.Object, contextAc.Object);

            /// Act
            var result = await sut.CreateReport(user) as OkObjectResult; 
            Assert.NotNull(result);

            var model = result.Value as LocationStatusReport;
            Assert.NotNull(model);

            var actual = model.status;
            Assert.Equal("Haz�rlan�yor", actual);
             

        }


    }

}
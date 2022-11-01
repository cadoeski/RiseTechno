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
using System;
using Contact.Application.Features.Users.Queries.GetContacsById;
using Report.Application.Features.Queries;

namespace TestProject1
{
    public class ContactApiTest
    {
        [InlineData("123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789", "test", "test2")]
        [InlineData("Þirket Adý", "Name", "surName")]
        [InlineData("Þirket Adý", "name", "U")]
        [InlineData("Þirket Adý", "n", "surname")]
        [InlineData("Þirket Adý", "n", "s")]
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
        [InlineData("Þirket Adý", "Name", "surName")]
        [InlineData("Þirket Adý", "name", "U")]
        [InlineData("Þirket Adý", "n", "surname")]
        [InlineData("Þirket Adý", "n", "s")]
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

        [InlineData("123","123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789", "test", "test2")]
        [InlineData("123", "Þirket Adý", "Name", "surName")]
        [InlineData("123", "Þirket Adý", "name", "U")]
        [InlineData("123", "Þirket Adý", "n", "surname")]
        [InlineData("123", "Þirket Adý", "n", "s")]
        [Theory]
        public async Task UpdateUser_ShouldReturn200(Guid i,string c, string n, string s)
        {
            /// Arrange
            var servie = new Mock<MediatR.IMediator>();
            var contextAc = new Mock<IHttpContextAccessor>();


            UserUpdateCommand user = new UserUpdateCommand();
            user.company = c;
            user.name = n;
            user.surname = s;
            user.uid = i;

            var sut = new ContactController(servie.Object, contextAc.Object);

            /// Act
            var result = await sut.UserUpdate(user) as OkObjectResult;


            // /// Assert 

            Assert.True(result.StatusCode.Value == 200);
            result.Should().NotBeNull();

        }

        [InlineData("123")] 
        [Theory]
        public async Task UpdateDelete_ShouldReturn200(string i)
        {
            /// Arrange
            var servie = new Mock<MediatR.IMediator>();
            var contextAc = new Mock<IHttpContextAccessor>();


            UserDeleteCommand user = new UserDeleteCommand(); 
            user.uid = i;

            var sut = new ContactController(servie.Object, contextAc.Object);

            /// Act
            var result = await sut.UserDelete(user) as OkObjectResult;


            // /// Assert 

            Assert.True(result.StatusCode.Value == 200);
            result.Should().NotBeNull();

        }


        [Fact]
        public async Task GetUser_ShouldReturn200()
        {
            /// Arrange
            var servie = new Mock<MediatR.IMediator>();
            var contextAc = new Mock<IHttpContextAccessor>();


            ReadContacsReport user = new ReadContacsReport(Guid.NewGuid()); 

            var sut = new ContactController(servie.Object, contextAc.Object);

            /// Act
            var result = await sut.GetUser(user) as OkObjectResult;
             
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
            Assert.Equal("Hazýrlanýyor", actual); 

        }

        [Fact] 
        public async Task GetReportStatus_Count()
        {
            /// Arrange
            var servie = new Mock<MediatR.IMediator>();
            var contextAc = new Mock<IHttpContextAccessor>();


            LocationStatusGetReport report = new LocationStatusGetReport();
            report.reportId = Guid.NewGuid();

            var sut = new ReportController(servie.Object, contextAc.Object);

            /// Act
            var result = await sut.GetReportStatus(report) as OkObjectResult;
            Assert.NotNull(result);
             
            var model = result.Value as LocationStatusReport;
            Assert.NotNull(model);

            var actual = model.status;
            Assert.Equal("Hazýrlanýyor", actual); 
        }

    }

}
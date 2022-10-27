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
            user.company = "h";
            user.name = "hata";
            user.surname = "surname";

            var sut = new ContactController(servie.Object, contextAc.Object);

            /// Act
            var result = await sut.UserCreate(user) as OkObjectResult;


            // /// Assert 
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
            
        }




    }

}
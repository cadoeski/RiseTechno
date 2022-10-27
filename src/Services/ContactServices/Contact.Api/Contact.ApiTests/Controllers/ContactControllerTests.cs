using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contact.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Application.Contracts.Persistence;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Contact.Application.Contracts.Infrastructure;
using Contact.Application.Features.Users.UserCreate;
using Moq;
using MassTransit.Mediator;
using Contracts.Contact;
using Xunit;

namespace Contact.Api.Controllers.Tests
{
    [TestClass()]
    public class ContactControllerTests
    {




        [Fact]
        public async void ContactControllerTest()
        {
            var mediator = new Mock<IMediator>();
            //Arange
            var userWriteRepository = new Mock<IUserWriteRepository>();
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<UserCreateCommandHandler>>();
            var messageQ = new Mock<IMessageQueue>();

            UserCreateCommand command = new UserCreateCommand();
            UserCreateCommandHandler handler = new UserCreateCommandHandler(userWriteRepository.Object, mapper.Object, logger.Object, messageQ.Object);
            command.surname = "";
            command.name = "";

            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Asert
            //Do the assertion

            //something like:
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void UserCreateTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void UserUpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UserDeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ContactCreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetLocationReportTest()
        {
            Assert.Fail();
        }
    }
}
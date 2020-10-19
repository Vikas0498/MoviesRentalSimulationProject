using Authenticate1Api;
using Authenticate1Api.Controllers;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Moq;
using NUnit.Framework;

namespace NUnitTestAuth
{
    public class Tests
    {
        private Mock<IRepository<Authenticate1>> _user;
        private Mock<IConfiguration> _config;
        private Mock<IAuthRepo> _auth1;
        private Authenticate2Controller _controller;

        [SetUp]
        public void Setup()
        {
            _config = new Mock<IConfiguration>();
            _user = new Mock<IRepository<Authenticate1>>();
            _auth1 = new Mock<IAuthRepo>();
            _controller = new Authenticate2Controller(_config.Object, _user.Object, _auth1.Object);
        }

        [Test]
        public void Login_WhenCalled_ReturnsOk()
        {
            Authenticate1 user = new Authenticate1()
            {
                Id = 1,
                Username = "Vikas",
                Pass = 1234
            };
            _auth1.Setup(r => r.AuthenticateUser(It.IsAny<Authenticate1>())).Returns(user);
            _auth1.Setup(r => r.GenerateJSONWebToken()).Returns("Token");

            var result = _controller.Login(user);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void Login_WhenCalled_ReturnsUnAuthorized()
        {
            Authenticate1 user = new Authenticate1()
            {
                Id = 1,
                Username = "Anuj",
                Pass = 1234
            };
            _auth1.Setup(r => r.AuthenticateUser(It.IsAny<Authenticate1>())).Returns(() => null);
            _auth1.Setup(r => r.GenerateJSONWebToken()).Returns("Token");

            var result = _controller.Login(user);

            Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
        }
    }
}
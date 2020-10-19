using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Moq;
using MovieRentingApi.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject2
{
    [TestFixture]
    public class Tests
    {
        private Mock<IRepository<MovieRent>> _menu;
       private MovieRentingApiController _controller;
      private Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _menu = new Mock<IRepository<MovieRent>>();
           _config = new Mock<IConfiguration>();
            _controller = new MovieRentingApiController(_menu.Object,_config.Object);
        }

        [Test]
        public void Get_WhenCalled_ReturnsListOfMenuItems()
        {

            _menu.Setup(repo => repo.GetAll()).Returns(new List<MovieRent> {new MovieRent()
                {
                    CustomerId = 1,
                    CustomerName = "Vikas",
                    //Price = 100,
                    Quantity = 3,
                    ReturnDate = Convert.ToDateTime("2020-10-17 00:00:00.0000000"),
                    MovieName = "Avenger",
                } });

            var result = _controller.Get();


            Assert.That(result.Count, Is.EqualTo(1));
        }

         [Test]
         public void Post_WhenCalled_ReturnsOk()
         {

             _menu.Setup(repo => repo.Add(It.IsAny<MovieRent>())).Verifiable();

             var result = _controller.Post(new MovieRent { });


             //Assert.AreEqual(200,result.StatusCode);
             Assert.That(result, Is.TypeOf<OkResult>());
         }
    }
}
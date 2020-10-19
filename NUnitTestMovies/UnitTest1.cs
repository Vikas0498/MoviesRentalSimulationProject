using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Moq;
using MoviesApi.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestMovies
{
    [TestFixture]
    public class Tests
    {
        private Mock<IRepository<Movie>> _menu;
        private MoviesApiController _controller;
        private Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _menu = new Mock<IRepository<Movie>>();
            _config = new Mock<IConfiguration>();
            _controller = new MoviesApiController(_menu.Object,_config.Object);
        }

        [Test]
        public void Get_WhenCalled_ReturnsListOfMenuItems()
        {

            _menu.Setup(repo => repo.GetAll()).Returns(new List<Movie> {new Movie()
                {
                    Id = 5,
                    Name = "Avenger",
                    Price = 100,
                    NumberInStocks = 5,
                    RealeaseDate = Convert.ToDateTime("2018-08-12 00:00:00.0000000"),
                    GenreId = 1,
                } });

            var result = _controller.Get();


            Assert.That(result.Count, Is.EqualTo(1));
        }

       /* [Test]
        public void Post_WhenCalled_ReturnsOk()
        {

            _menu.Setup(repo => repo.Add(It.IsAny<Movie>())).Verifiable();

            var result = _controller.Post(new Movie { });


            //Assert.AreEqual(200,result.StatusCode);
            Assert.That(result, Is.TypeOf<OkResult>());
        }*/
    }
}
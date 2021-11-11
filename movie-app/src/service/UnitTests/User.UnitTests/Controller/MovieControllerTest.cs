using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Movie.Api;
using Movie.Api.Core.Interface;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Linq;

namespace Movie.UnitTests
{
    public class MovieControllerTest
    {
     
        private readonly Mock<IMovieService> _service;
        private readonly Mock<ILogger<MovieController>> _logger;

        public MovieControllerTest()
        {
            _service = new Mock<IMovieService>();
            _logger = new Mock<ILogger<MovieController>>();



        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {

               var _controller = new MovieController(_logger.Object, _service.Object);

            // Act
            
              var okResult = _controller.GetList(0,1);

            // Assert
            var items = Assert.IsType<List<Movie.Api.Movie>>(okResult);
            Assert.Equal(1, items.Count);
            
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var _controller = new MovieController(_logger.Object, _service.Object);

            // Act
            var okResult = _controller.GetList(0, 1000);

            // Assert
            var items = Assert.IsType<List<Movie.Api.Movie>>(okResult);
            Assert.Equal(1000, items.Count);
        }


        [Fact]
        public void Get_WhenCalled_ReturnsAOneItems()
        {
            // Act
            var _controller = new MovieController(_logger.Object, _service.Object);

            _service.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(GetFakeMovie()));
            // Act
            var okResult = _controller.Get(100);

            // Assert
            var items = Assert.IsType<Movie.Api.Movie>(okResult);
            Assert.NotNull(okResult);
            Assert.Equal(10, items.Id);
        }

        private Movie.Api.Movie GetFakeMovie()
        {
            return new Movie.Api.Movie()
            {
                Id = 10,
                Name = "Orange",
                Director = "D",
                Producer = "Male",
                Release = "orange@test.com",
                Hit = true
            };

        }
    }
}

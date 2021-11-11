using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CinemaProxy;
using Infrastructure.Provider.Interface;
using Microsoft.Extensions.Logging;
using Moq;
using Movie.Api.AutoMapper;
using Movie.Api.Core.Implementation;
using Movie.Api.Core.Interface;
using Xunit;

namespace Movie.UnitTests.Core
{
    public class MovieServiceTest
    {

        private readonly Mock<ICacheProvider> _cacheService;
        private readonly Mock<ILogger<MovieService>> _logger;
        private readonly Mock<ICinemaClient> _cinemaService;

        private readonly IMapper _mapper;
        public MovieServiceTest()
        {
            _logger = new Mock<ILogger<MovieService>>();
            _cacheService = new Mock<ICacheProvider>();
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c => {
                c.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfiguration.CreateMapper();
            _cinemaService = new Mock<ICinemaClient>();
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {

            var movieService = new MovieService(_logger.Object, _mapper, _cacheService.Object, _cinemaService.Object);



            // Act
            var okResult = movieService.GetList();

            // Assert
            var items = Assert.IsType<List<Movie.Api.Movie>>(okResult);
            Assert.Equal(100, items.Count);

        }

        
    }
}

using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie.Api.Core.Interface;
using Movie.Api.Entities;
using Infrastructure.Provider.Interface;
using Microsoft.Extensions.Caching.Memory;
using CinemaProxy;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Movie.Api.Core.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly ILogger<MovieService> _logger;
        private readonly IMapper _mapper;
        private readonly ICinemaClient _cinemaClient;
        private readonly ICacheProvider _cacheProvider;

      

       
         
        
        public MovieService(ILogger<MovieService> logger, IMapper mapper, ICacheProvider cacheProvider, ICinemaClient cinemaClient)
        {
            _logger = logger;
            _mapper = mapper;
            _cacheProvider = cacheProvider;
            _cinemaClient = cinemaClient;


        }

        public async Task<List<Movie>> GetList()
        {
            var response = new List<Movie>();
            try
            {

                var cacheKey = $"GetList:{typeof(Movie)}";
                response = _cacheProvider.GetFromCache<List<Movie>>(cacheKey);

                if (response != null)
                {
                    _logger.LogInformation("Return Cache GetList Movie");
                    return response;
                }

                _logger.LogInformation($"Start Cinema Service Call");
               
                var resultList = await _cinemaClient.CinemaAllAsync(0, 1000);
                _logger.LogInformation($"Start {this.GetType().Name} Movie");
                response = _mapper.Map<List<Movie>>(resultList);


                if (response!=null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(24));
                    _logger.LogInformation($"Cache Movie in {cacheKey}");
                    _cacheProvider.SetCache(cacheKey, response, cacheEntryOptions);
                    _logger.LogInformation($"Comeplete {this.GetType().Name} Movie");
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
            }
            return response;
        }

        public async Task<Movie> Get(int Id)
        {
            var response = new Movie();

            try
            {

                _logger.LogInformation($"Start {this.GetType().Name} Movie");

                var cacheKey = $"get:{typeof(Movie)}";
                response = _cacheProvider.GetFromCache<Movie>(cacheKey);

                if (response != null)
                {
                    _logger.LogInformation("Return Cache Movie");
                    return response;
                }
                
                 var dtoMovie = await GetList();
                 response = _mapper.Map<Movie>((Movie)dtoMovie.Where(x=>x.Id==Id).FirstOrDefault());

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _logger.LogInformation($"Cache Movie in {cacheKey}");
                _cacheProvider.SetCache(cacheKey, response, cacheEntryOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
            }


            return response;
        }

        public async Task<bool> Insert(Movie Movie)
        {
            try
            {
                _logger.LogInformation($"Start {this.GetType().Name} Movie");
                var movieData = _mapper.Map<CinemaModel>(Movie);

                var response = await _cinemaClient.CinemaPOSTAsync(movieData);
                

                _logger.LogInformation($"Completed {this.GetType().Name} Movie");
                return response;
            }
            catch (Exception ex)
            {
              
                _logger.LogError("Exception", ex.Message);
    
                return false;
            }
        }

        

        public async Task<bool> Update(Movie Movie)
        {
            try
            {
                _logger.LogInformation($"Start {this.GetType().Name} Movie");
                var movieData = _mapper.Map<CinemaModel>(Movie);

                await _cinemaClient.CinemaPUTAsync(movieData.Id, movieData);

             

                _logger.LogInformation($"Completed {this.GetType().Name} Movie");
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError("Exception", ex.Message);

                return false;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                _logger.LogInformation($"Start {this.GetType().Name} Movie");
             
                 await _cinemaClient.CinemaDELETEAsync(Id);

                _logger.LogInformation($"Completed {this.GetType().Name} Movie");
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError("Exception", ex.Message);

                return false;
            }
        }

       
    }
}

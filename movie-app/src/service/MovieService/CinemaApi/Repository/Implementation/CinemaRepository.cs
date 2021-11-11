using CinemaApi.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace CinemaApi.Repository.Implementation
{
    public class CinemaRepository : ICinemaRepository
    {

        private readonly ILogger<CinemaRepository> _logger;

        public CinemaRepository(ILogger<CinemaRepository> logger)
        {
            _logger = logger;
        }

        public void Delete(int Id)
        {
            try
            {
                _logger.LogInformation("Start Delete");

                throw new NotImplementedException();
                _logger.LogInformation("Completed Delete");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
            }

        }

      

        public async Task<IEnumerable<CinemaDto>> GetList()
        {
            var result = new List<CinemaDto>();
            try
            {
               

                _logger.LogInformation("Start Get");

                 result = GetMovies();

                _logger.LogInformation("Completed Get");
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
               
            }
            return result;  
        }

        public async Task<CinemaDto> GetMovieByID(int Id)
        {
            try
            {
                _logger.LogInformation("Start GetMovieByID");

                var result = GetMovies()?.Where(x=>x.Id.Equals(Id))?.FirstOrDefault();

              
              
                _logger.LogInformation("Completed GetMovieByID");
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
                throw;
            }
        }

        public void Insert(CinemaDto Movie)
        {
            try
            {
                _logger.LogInformation("Start Insert");

                throw new NotImplementedException();
                _logger.LogInformation("Completed Insert");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
                throw;
            }
        }

        public void Save(CinemaDto Movie)
        {
            try
            {
                _logger.LogInformation("Start Save");

                throw new NotImplementedException();
                _logger.LogInformation("Completed Save");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
                throw;
            }
        }

        public void Update(CinemaDto Movie)
        {
            try
            {
                _logger.LogInformation("Start Update");

                throw new NotImplementedException();
                _logger.LogInformation("Completed Update");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
                throw;
            }
        }

        private List<CinemaDto> GetMovies()
        {
            try
            {
                var response =  new List<CinemaDto>();
                _logger.LogInformation("Start GetMovies");
                //..\\..\\junk\\
                using (StreamReader r = new StreamReader(@".\Repository\Data\MOCK_DATA.json"))
                {
                    string jsonString = r.ReadToEnd();
                    response = JsonConvert.DeserializeObject<List<CinemaDto>>(jsonString);

                };

                
                _logger.LogInformation("Completed GetMovies");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
                throw;
            }
        }

    }
}

using AutoMapper;
using CinemaApi.Core.Interface;
using CinemaApi.Entities;
using CinemaApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CinemaApi.Core.Implementation
{
    public class CinemaService : ICinemaService
    {
        private readonly ILogger<CinemaService> _logger;
        private readonly IMapper _mapper;
        private readonly ICinemaRepository _cinemaRepository;
        public CinemaService(ILogger<CinemaService> logger, IMapper mapper, ICinemaRepository cinemaRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _cinemaRepository = cinemaRepository;
          
        }


        public async Task<List<CinemaModel>> GetList()
        {
            var response = new List<CinemaModel>();
            try
            {
                _logger.LogInformation($"Start Get CinemaList {DateTimeOffset.Now.ToString()}");

                var dtoList = await _cinemaRepository.GetList();
                response = _mapper.Map<List<CinemaModel>>(dtoList);

                _logger.LogInformation($"Completed Get CinemaList {DateTimeOffset.Now.ToString()}");


            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
            }
            return response;
        }

        public async Task<CinemaModel> Get(int Id)
        {
            var response = new CinemaModel();
            try
            {
                _logger.LogInformation("Start Get Cinema");
                var dtoMovie = await _cinemaRepository.GetMovieByID(Id);
                response = _mapper.Map<CinemaModel>(dtoMovie);
                _logger.LogInformation("Completed Get Cinema");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex.Message);
            }
            return response;
        }

        public bool Insert(CinemaModel cinema)
        {
            try
            {
                _logger.LogInformation("Start Get Insert");
                var MovieData = _mapper.Map<CinemaDto>(cinema);
                _cinemaRepository.Insert(MovieData);
               
                _logger.LogInformation("Completed Get Insert");
                return true;
            }
            catch (Exception ex)
            {
              
                _logger.LogError("Exception", ex.Message);
    
                return false;
            }
        }

        public bool Save(CinemaModel cinema)
        {
            try
            {
                _logger.LogInformation("Start Get Save");
                var MovieData = _mapper.Map<CinemaDto>(cinema);
                _cinemaRepository.Save(MovieData);

                _logger.LogInformation("Completed Get Save");
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError("Exception", ex.Message);

                return false;
            }
        }

        public bool Update(CinemaModel cinema)
        {
            try
            {
                _logger.LogInformation("Start Get Update");
                var MovieData = _mapper.Map<CinemaDto>(cinema);
                _cinemaRepository.Update(MovieData);

                _logger.LogInformation("Completed Get Update");
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError("Exception", ex.Message);

                return false;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                _logger.LogInformation("Start Get Delete");

                _cinemaRepository.Delete(Id);

                _logger.LogInformation("Completed Get Delete");
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

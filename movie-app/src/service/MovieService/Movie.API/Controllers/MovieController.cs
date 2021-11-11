using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Movie.Api.Core.Interface;
using Movie.Api.Entities;

namespace Movie.Api
{

    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;

        private readonly IMovieService _movieService;
        public MovieController(ILogger<MovieController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }


        [Route("api/movies"), HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetList(int Page = 0, int Size = 25)
        {


            _logger.LogInformation("start GetList");
            var result = new List<Movie>();
            try
            {

                result = await _movieService.GetList();
                result = result?.Skip(Size * Page)?.Take(Size)?.ToList();

            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Movies", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            _logger.LogInformation("Completed GetList");
            return result;

        }

        // GET: api/Employee/5
        [HttpGet("api/movie/{id}", Name = "Get")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            _logger.LogInformation("start Get");

            if (id <= 0 || id > 1000)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var result = new Movie();
            try
            {
                result = await _movieService.Get(id);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error Movies", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            _logger.LogInformation(" End Get ");

            return result;
        }

        // POST: api/Employee
        [HttpPost, Route("api/movie")]
        [Produces("application/json")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<bool>> Post([FromBody] Movie MovieData)
        {
            if (MovieData == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation(" Insert Movie");

            try
            {
                var result = await _movieService.Insert(MovieData);
                return result;

            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Movies", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        // PUT: api/Employee/5
        [HttpPut("api/movie/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Movie MovieData)
        {


            _logger.LogInformation(" Insert Movie");
            if (id <= 0 || id > 1000)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var result = await _movieService.Update(MovieData);

                return result;

            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Movies", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("api/movie/{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {

            if (id <= 0 || id > 1000)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation(" Insert Movie");

            try
            {
                var result = await _movieService.Delete(id);

                return result;

            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Movies", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }

}


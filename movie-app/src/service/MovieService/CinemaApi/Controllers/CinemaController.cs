using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CinemaApi;
using CinemaApi.Core.Interface;
using System.Net.Mime;

namespace CinemaApi
{

    [ApiController]
    public class CinemaController : ControllerBase
    {

        private readonly ILogger<CinemaController> _logger;

        private readonly ICinemaService _cinemaService;
        public CinemaController(ILogger<CinemaController> logger, ICinemaService cinemaService)
        {
            _logger = logger;
            _cinemaService = cinemaService;
        }


        [Route("api/cinema"), HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CinemaModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CinemaModel>>> GetList(int Page = 0, int Size = 25)
        {


            _logger.LogInformation("start Cinema GetList");
            var result = new List<CinemaModel>();
            try
            {

                result = await _cinemaService.GetList();
                result = result?.Skip(Size * Page)?.Take(Size)?.ToList();

            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Cinema GetList", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            _logger.LogInformation("Completed GetList");
            return result;

        }

        // GET: api/Employee/5
        [HttpGet("api/cinema/{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CinemaModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CinemaModel>> Get(int id)
        {
            _logger.LogInformation("start Get");

            if (id <= 0 || id > 1000)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var result = new CinemaModel();
            try
            {
                result = await _cinemaService.Get(id);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error Get Cinema ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            _logger.LogInformation(" End Get ");

            return result;
        }

        // POST: api/Employee
        [HttpPost, Route("api/cinema")]
        [ValidateAntiForgeryToken]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> Post([FromBody] CinemaModel cinemaData)
        {
            if (cinemaData == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation(" Insert Cinema");

            try
            {
                var result = _cinemaService.Insert(cinemaData);
                return result;

            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Creating Cinema", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        // PUT: api/Employee/5
        [HttpPut("api/cinema/{id}")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> Put(int id, [FromBody] CinemaModel cinemaData)
        {


            _logger.LogInformation(" Insert Movie");
            if (id <= 0 || id > 1000)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var result = _cinemaService.Update(cinemaData);

                return result;

            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Update cinema", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("api/cinema/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> Delete(int id)
        {

            if (id <= 0 || id > 1000)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation(" Insert cinema");

            try
            {
                var result = _cinemaService.Delete(id);

                return result;

            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Delete cinema", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }

}


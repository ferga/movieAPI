using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using movieAPI.Entities;
using movieAPI.Repositories;

namespace movieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetadataController : ControllerBase
    {

        // private readonly ILogger<MoviesController> _logger;              No use of logging in this example
        IMovies _moviesData;

        public MetadataController(IMovies movies)
        {
            //_logger = logger;                                         // No logging unfortunately
            _moviesData = movies;
        }

        [HttpGet("{OrderRef}")]
        public List<MovieMeta> Order(int movieRef)
        {
            List<MovieMeta> results=_moviesData.GetMoviesMeta(movieRef);
            // we need to return results ordered alphabetically by language
            results=results.OrderBy(x => x.Language).ToList();
            return results;
        }

        [HttpPost]
        public IActionResult saveMeta(MovieMeta movie)
        {
            try
            {
                int newMovieCount=_moviesData.addMovieMeta(movie);

                // return basic user info and authentication token
                return Ok(new { Token = newMovieCount });
            }
            catch (Exception ex)
            {
                //switch (ex.Message) {
                //    case constMessages.wrongPassword:

                return BadRequest(new { message = ex.Message });
                //default:
                //}

            }

        }
    }
}

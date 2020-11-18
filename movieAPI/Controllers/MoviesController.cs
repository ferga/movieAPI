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
    public class MoviesController : ControllerBase
    {
         
        // private readonly ILogger<MoviesController> _logger;              No use of logging in this example
        IMovies _moviesData;

        public MoviesController(IMovies movies)
        {
            //_logger = logger;                                         // No logging unfortunately
            _moviesData = movies;
        }

        [HttpGet("stats")]
        public List<MovieStat> Order()
        {
            // The movies are ordered by most watched, then by release year with newer releases being
            // considered more important
            List < MovieStat > results=_moviesData.GetMoviesStats();
            results = results.OrderBy(x => x.WatchCount).ThenByDescending(y => y.releaseYear).ToList();

            return results;
        }
         
         
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using movieAPI.Entities;

namespace movieAPI.Repositories
{
    interface IMovies
    {
        
        List<MovieMeta> GetMoviesMeta();

        List<MovieStat> GetMoviesStats();
        
    }

    class Movies:IMovies
    {
        List<MovieMeta> moviesWithMeta;
        List<MovieStat> moviesWithStat;

        public Movies()
        {

        }

        public List<MovieMeta> GetMoviesMeta()
        {
            List<MovieMeta> toReturn = new List<MovieMeta>();
            // For another implementation, we might needed some more 
            // process of the list before sending the data, but not this time
            return moviesWithMeta;

        }

        public List<MovieStat> GetMoviesStats()
        {
            List<MovieStat> toReturn = new List<MovieStat>();
            // For another implementation, we might needed some more 
            // process of the list before sending the data, but not this time
            return moviesWithStat;


        }
    }
}

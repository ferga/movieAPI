using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace movieAPI.Entities
{
    [JsonObject]
    [Serializable]
    public class MovieStat:Movie
    {
        
        int _avgWatchDurationMs;
        int _watchCount;

        /// <summary>
        /// A contractor to instantiate all properties, apart from the base class's
        /// </summary>
        /// <param name="movieId">the movie ID</param>
        /// <param name="watchDurationMs">the Duration the movie was viewed</param>
        /// <param name="watchCount">the amount of viewers</param>
        public MovieStat(int movieId, int watchDurationMs, int watchCount)
        {
            MovieId = movieId;
            _avgWatchDurationMs = watchDurationMs;
            _watchCount = watchCount;
        }

        public MovieStat(int movieId, string title, string releaseYear, int watchDurationMs, int watchCount)
        {
            MovieId = movieId;
            Title = title;
            ReleaseYear = releaseYear;
            _avgWatchDurationMs = watchDurationMs;
            _watchCount = watchCount;
        }

        public int WatchDurationMs { get => _avgWatchDurationMs; set => _avgWatchDurationMs = value; }
        public int WatchCount { get => _watchCount; set => _watchCount = value; }
    }
}

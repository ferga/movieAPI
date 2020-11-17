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

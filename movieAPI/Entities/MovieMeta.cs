using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    using Newtonsoft.Json;

namespace movieAPI.Entities
{
    [JsonObject]
    [Serializable]
    public class MovieMeta:Movie
    {
        string _language;
        string _duration;

        public MovieMeta()
        {
        }

        public MovieMeta(int movieId, string title,  string language, string duration, string releaseYear)
        {
            MovieId = movieId;
            Title = title;
            ReleaseYear = releaseYear;
            _language = language;
            _duration = duration;
        }

        public string Language { get => _language; set => _language = value; }
        public string Duration { get => _duration; set => _duration = value; }
    }
}

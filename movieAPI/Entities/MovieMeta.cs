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

        public MovieMeta(int movId, string ttl,  string language, string durn, string ReleaseYear)
        {
            movieId = movId;
            title = ttl;
             releaseYear=Convert.ToInt32(ReleaseYear );
            _language = language;
            _duration = durn;
        }

        public string language { get => _language; set => _language = value; }
        public string duration { get => _duration; set => _duration = value; }
    }
}

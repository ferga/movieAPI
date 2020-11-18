 
namespace movieAPI.Entities
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject]
    [Serializable]
    public class Movie
    {
        int _movieId;
        string _title;
        int _releaseYear;


        public Movie()
        {

        }

        public Movie(int movId, string ttl, int relYear)
        {
            movieId = movId;
            title = ttl;
            releaseYear = relYear;
            
        }

        public int movieId { get => _movieId; set => _movieId = value; }
        public string title { get => _title; set => _title = value; } 
        public int releaseYear { get => _releaseYear; set => _releaseYear = value; }
    }
}

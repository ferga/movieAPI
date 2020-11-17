 
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
        string _releaseYear;


        public Movie()
        {

        }

        public Movie(int movieId, string title, string releaseYear)
        {
            MovieId = movieId;
            Title = title;
            ReleaseYear = releaseYear;
            
        }

        public int MovieId { get => _movieId; set => _movieId = value; }
        public string Title { get => _title; set => _title = value; }
        public string ReleaseYear { get => _releaseYear; set => _releaseYear = value; }
    }
}

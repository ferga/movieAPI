using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using movieAPI.DataLayer;
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
        struct rawStats
        {
            int movieID;
            int DurationMs;
        }
        const string metaFileLocation = "../DataFiles/metadata.csv";        // Normally, I would have this information placed in the Appsettings.Json
        const string statsFileLocation = "../DataFiles/stats.csv";        // Normally, I would have this information placed in the Appsettings.Json

        List<MovieMeta> moviesWithMeta;
        List<MovieStat> moviesWithStat;

        public Movies()
        {
            List<string> metaStrings = Retrieve.RetrieveData(metaFileLocation);
            populateMovieLists();
        }

        private List<MovieMeta> getMetaFromString()
        {
            List<MovieMeta> dataList=new List<MovieMeta>();
            return dataList;

        }

        //private List<MovieStat> getStatsFromString()
        private void populateMovieLists()
        {
            List<string> statsStrings = Retrieve.RetrieveData(statsFileLocation);
            List<string> metaStrings = Retrieve.RetrieveData(metaFileLocation);
             
            statsStrings.Sort();        // Sort the list so all ids are together
             
            int currentID = -1;
            int numOfEntries = 0;
            double sumOfMs = 0;
            foreach (string statString in statsStrings)
            {
                int newID = Convert.ToInt32((statString.Split(','))[0]);
                double currentMs = Convert.ToDouble((statString.Split(','))[1]);
                if ((newID != currentID) && (numOfEntries > 0))           //  a) we can't divide by zero b) we only want to add a stat after we got an entry
                {
                    // Argument is int, as the average watch time, shouldn't be more than the movies duration
                    MovieStat aStat = new MovieStat(currentID, (int)(sumOfMs / numOfEntries), numOfEntries);
                    moviesWithStat.Add(aStat);
                    numOfEntries = 0;
                    sumOfMs = currentMs;
                }
                else
                {
                    sumOfMs += currentMs;                               // Add entry to the sum of stats
                    numOfEntries++;
                }
                currentID = newID;                                  // Set the id of the new statString element
            }
            // There should be one last item we haven't inserted to the list
            if (numOfEntries != 0)
            {
                // Argument is int, as the average watch time, shouldn't be more than the movies duration
                MovieStat aStat = new MovieStat(currentID, (int)(sumOfMs / numOfEntries), numOfEntries);
                moviesWithStat.Add(aStat);
            }

            // Now we have to retrieve the metaData and use them to populate the missing
            // data in the statsList
            foreach (string meta in metaStrings)
            {
                string[] metaArray = meta.Split(',');
                // MetaArray[0] should be the file id
                MovieMeta metaItem = new MovieMeta(Convert.ToInt32(metaArray[1]), metaArray[2], metaArray[3], metaArray[4], metaArray[5]);
                moviesWithMeta.Add(metaItem);
                var obj = moviesWithStat.FirstOrDefault(statList => statList.MovieId == metaItem.MovieId);
                // obj is a reference type like the MovieStat inside the list so, the above assignment is a reference copy
                // thus, any change in the obj values, will be a change of the values inside the list...
                obj.Title = metaItem.Title;
                obj.ReleaseYear = metaItem.ReleaseYear;

            }

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

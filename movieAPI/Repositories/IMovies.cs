using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using movieAPI.DataLayer;
using movieAPI.Entities;

namespace movieAPI.Repositories
{
    public interface IMovies
    {
        
        List<MovieMeta> GetMoviesMeta(int movieRef);
        List<MovieStat> GetMoviesStats();
        int addMovieMeta(MovieMeta newMeta);
    }

    public class Movies:IMovies
    {
        struct rawStats
        {
            int movieID;
            int DurationMs;
        }
        const string metaFileLocation = "../DataFiles/metadata.csv";        // Normally, I would have this information placed in the Appsettings.Json
        const string statsFileLocation = "../DataFiles/stats.csv";        // Normally, I would have this information placed in the Appsettings.Json

        private List<MovieMeta> moviesWithMeta;
        private List<MovieStat> moviesWithStat;

        public Movies()
        {
            moviesWithMeta = new List<MovieMeta>();
            moviesWithStat = new List<MovieStat>();
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
            // metaStrings  are already sorted by id and MovieID
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

            MovieMeta previousMeta = new MovieMeta();
            // Now we have to retrieve the metaData and use them to populate the missing
            // data in the statsList
            foreach (string meta in metaStrings)
            {
                string[] metaArray = meta.Split(',');
                // We will not consider metadata with missing values
                if (metaArray.Length > 5)
                {
                // MetaArray[0] should be the file id
                    MovieMeta metaItem = new MovieMeta(Convert.ToInt32(metaArray[1]), metaArray[2], metaArray[3], metaArray[4], metaArray[5]);
                    // The first item as the previous and the current one
                    if (previousMeta == null)
                        previousMeta = metaItem;
                    else
                    // Make sure there are no empty values
                    if (metaItem.MovieId != 0 && metaItem.Title != "" && metaItem.Language != "" && metaItem.Duration != "" && metaItem.ReleaseYear != "")
                    {
                        // We have to take only the latest (higher ID number) of the same metadata
                        // as "same", we consider metadata that have the same movieID and Language parameters
                        if ((metaItem.MovieId != previousMeta.MovieId) || (metaItem.Language != previousMeta.Language))
                        {// We have a new meta data that needs to be stored, 
                         //  previousMeta, have the latest values of similar metas, since the list ordered by id and movieID
                            moviesWithMeta.Add(metaItem);
                            previousMeta = metaItem;        // Update the previous meta as we have added it to the list
                            // Add the missing data in the stats list objects
                            var obj = moviesWithStat.FirstOrDefault(statList => statList.MovieId == metaItem.MovieId);
                            // obj is a reference type like the MovieStat inside the list so, the above assignment is a reference copy
                            // thus, any change in the obj values, will be a change of the values inside the list...
                            if (obj != null)        // There might extra movieIDs that do not exist in the metadata file
                            {
                                obj.Title = metaItem.Title;
                                obj.ReleaseYear = metaItem.ReleaseYear;
                            }
                        }
                    }
                }
            }

        }

        public List<MovieMeta> GetMoviesMeta(int movieID)
        {
            List<MovieMeta> toReturn = new List<MovieMeta>();
            // For another implementation, we might needed some more 
            // process of the list before sending the data, but not this time
            return moviesWithMeta.FindAll(x=>x.MovieId==movieID);

        }

        /// <summary>
        /// It adds a new MovieMeta object to the list
        /// </summary>
        /// <param name="newMeta">the object to be added to the list</param>
        /// <returns>An int, that is the count of the list of meta, after 
        /// adding the new one</returns>
        public int addMovieMeta(MovieMeta newMeta)
        {
            moviesWithMeta.Add(newMeta);

            return moviesWithMeta.Count();
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

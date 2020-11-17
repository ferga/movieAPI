using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieAPI.DataLayer
{

    class fromText : IDataRetriever
    {
        public List<string> RetrieveData(string filePath)
        {
            List<string> records = new List<string>();
            string line;
            System.IO.StreamReader file;
            try
            {
                // Read the file and load it line by line.  
                file = new System.IO.StreamReader(filePath);
                while ((line = file.ReadLine()) != null)
                {
                    records.Add(line);
                }
                file.Close();
            }
            catch (Exception ex)
            {
                // throw ex         // We will enable that on a later time, so we can have the main application handle the exceptions
                return null;        // Let us know we couldn't retrieve any records
            }
            finally
            {
                ///
            }
            return records;
        }
    }
}

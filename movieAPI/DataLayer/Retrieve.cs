using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieAPI.DataLayer
{
    interface IDataRetriever
    {
        List<string> RetrieveData(string dataSrcString);
    }

    public static class Retrieve
    {
        public static List<string> RetrieveData(  string dataSrcConnection)
        {
            IDataRetriever dtRetriever;
            List<string> records;

            if (dataSrcConnection.Contains(".csv"))
            {
                dtRetriever = new fromText();
                records = dtRetriever.RetrieveData(dataSrcConnection); 
            } 
            else
                throw new NotImplementedException();
            
            return records;
        }
    }
}

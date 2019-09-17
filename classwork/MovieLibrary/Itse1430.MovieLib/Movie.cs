using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itse1430.MovieLib
{
    /// <summary>
    /// Represents movie data.
    /// </summary>
    /// <remarks></remarks>
    public class Movie
    {
        //never make fields public!!
        public string title = "";
        public string description = "";
        public int releaseYear = 1900;
        public string rating = "";
        public bool hasSeen;
        public int runLength;

        //can new up other objects
        //private Movie originalMovie = new Movie ();
    }
}

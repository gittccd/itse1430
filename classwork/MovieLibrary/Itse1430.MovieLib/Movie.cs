using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itse1430.MovieLib
{
    /// <summary>Represents movie data.</summary>
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

        /// <summary>
        /// Validates the movie.</summary>
        /// <returns>An error msg if validation fails or empty string otherwise.</returns>

        public string Validate()
        {
            var title = "";
            //name is required
            if (String.IsNullOrEmpty (this.title))
                return "Title is required.";
            //releaseyear >= 1900
            if (releaseYear < 1900)
                return "Release Year must be >= 1900.";
            //run length >= 0
            if (runLength < 0)
                return "Run Length must be >= 0";
            //rating is required
            if (String.IsNullOrEmpty (rating))
                return "Rating is required";
            return "";
        }
        //can new up other objects
        //private Movie originalMovie = new Movie ();
    }
}

using System.Collections.Generic;
using System.Linq;
using System;
namespace Itse1430.MovieLib
{
    public abstract class MovieDatabase : IMovieDatabase
    {


        public Movie Add ( Movie movie )
        {
            //TODO: validation
            if (movie == null)
                return null;
            //if (!String.IsNullOrEmpty (movie.Validate ()))
            //var context = new ValidationContext (movie);

            //var results = movie.Validate (context);

            var results = ObjectValidator.TryValidateObject (movie);

            if (results.Count () > 0)
                return null;

            //name must be unique
            var existing = GetByNameCore (movie.Title);
            if (existing != null)
                return null;

            return AddCore (movie);
        }

        /// <summary>
        /// add movie to database
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>updated movie</returns>
        /// 
        protected abstract Movie AddCore ( Movie movie );
        public void Remove ( int id )
        {
            //validate the id
        
                RemoveCore (id);
          
        }

        protected abstract void RemoveCore ( int id );

        public Movie Get ( int id )
        {
            //TODO: validate
            if (id <= 0)
                return null;

            return GetCore (id);
            
        }

        protected abstract Movie GetCore ( int id );

        public IEnumerable<Movie> GetAll ()
        {
            return GetAllCore ();
            
        }

        protected abstract IEnumerable<Movie> GetAllCore ();

        public void Update ( int id, Movie newMovie )
        {
            //validate parameters
            if (id <= 0)
                return;
            if (newMovie == null)
                return;

            //if (!String.IsNullOrEmpty(newMovie.Validate()))
            var results = ObjectValidator.TryValidateObject(newMovie);
           
            if (results.Count () > 0)
                return;

            var existing = GetByNameCore (newMovie.Title);
            if (existing != null && existing.Id != id)
                return;

            UpdateCore (id, newMovie);
        }

        protected abstract Movie UpdateCore( int id, Movie newMovie );



        protected abstract Movie GetByNameCore ( string name );
                  
        
    }
}
